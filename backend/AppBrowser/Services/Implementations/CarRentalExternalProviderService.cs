using System.Text;
using System.Text.Json;
using AppBrowser.DTOs;
using AppBrowser.Infrastructure;
using AppBrowser.Model;
using AppBrowser.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AppBrowser.Services.Implementations;

// TODO: create ICarRentalExternalProviderService interface
// This is a service that will communicate with our own public API
// Name might be changed later
public class CarRentalExternalProviderService : IExternalProviderService
{
    private readonly IConfiguration _configuration;
    private readonly HttpClient _httpClient;
    private readonly DataContext _context;
    private readonly ILogger<CarRentalExternalProviderService> _logger;

    private const int CarRentalExternalProviderId = 1;
    
    public CarRentalExternalProviderService(IConfiguration configuration, HttpClient httpClient, DataContext context, ILogger<CarRentalExternalProviderService> logger)
    {
        _configuration = configuration;
        _httpClient = httpClient;
        _context = context;
        _logger = logger;
    }

    public int GetProviderId()
    {
        return CarRentalExternalProviderId;
    }

    public async Task<List<Car>> SearchCars(string brandName, string modelName)
    {
        string url = $"{_configuration.GetValue<string>("CarRentalBaseAPIUrl")!}/api/cars";
        var response = await _httpClient.GetAsync(url);
        
        if (!response.IsSuccessStatusCode) throw new HttpRequestException("Cannot fetch cars from car rental API");
        
        var json = await response.Content.ReadAsStringAsync();
        var cars = JsonSerializer.Deserialize<List<CarRentalExternalProviderCarDto>>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
        if (cars == null)
        {
            return [];
        }
        var carsDto = cars.Select((c) => CarFromProviderDto.MapFromCarRentalProvider(c, GetProviderId())).ToList();

        var browserCars = await SyncCarsInDB(carsDto);
        
        if (brandName == "" && modelName == "")
        {
            return browserCars;
        }

        if (brandName == "")
        {
            // filter by model name
            return browserCars.Where(c => c.ModelName.Contains(modelName, StringComparison.CurrentCultureIgnoreCase)).ToList();
        }
        
        if (modelName == "")
        {
            // filter by brand name
            return browserCars.Where(c => c.BrandName.Contains(brandName, StringComparison.InvariantCultureIgnoreCase)).ToList();
        }
        
        // filter by both
        return browserCars.Where(c => c.BrandName.Contains(brandName, StringComparison.InvariantCultureIgnoreCase) && c.ModelName.Contains(modelName, StringComparison.InvariantCultureIgnoreCase)).ToList();
    }

    public async Task<List<Car>> SyncCarsInDB(List<CarFromProviderDto> cars)
    {
        var providerId = GetProviderId();
        var browserCars = new List<Car>();
        foreach (var car in cars)
        {
            var carInDb = await _context.Cars.FirstOrDefaultAsync(c => c.ExternalCarId == car.CarId && c.ProviderId == providerId);
            if (carInDb != null)
            {
                // Car already in DB - update it to have current state
                carInDb.BrandName = car.BrandName;
                carInDb.ModelName = car.ModelName;
                carInDb.Location = car.Localization;
                carInDb.ProductionYear = car.ProductionYear;
                carInDb.ImageUrl = car.ImageUrl;
                browserCars.Add(carInDb);
            }
            else
            {
                // New car - need to insert it into DB
                var newCar = new Car
                {
                    ExternalCarId = car.CarId,
                    ProviderId = providerId,
                    BrandName = car.BrandName,
                    ModelName = car.ModelName,
                    Location = car.Localization,
                    ProductionYear = car.ProductionYear,
                    ImageUrl = car.ImageUrl,
                };
                await _context.Cars.AddAsync(newCar);
                browserCars.Add(newCar);
            }
        }
        await _context.SaveChangesAsync();
        return browserCars;
    }

    public async Task<CarRentalExternalProviderOfferDto> GetOffer(CarRentalExternalProviderCreateOfferDto createOfferDto)
    {
        string url = $"{_configuration.GetValue<string>("CarRentalBaseAPIUrl")}/api/offer";
        var json = JsonSerializer.Serialize(createOfferDto);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        
        var response = await _httpClient.PostAsync(url, content);
        _logger.LogInformation("Response status code: {}", response.StatusCode);
        
        if (!response.IsSuccessStatusCode) throw new HttpRequestException("Couldnt get offer from external provider");
        json = await response.Content.ReadAsStringAsync();
        var offerDto = JsonSerializer.Deserialize<CarRentalExternalProviderOfferDto>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
        if (offerDto == null)
            throw new HttpRequestException("Coulnd't parse body from external provider");
        return offerDto;
    }

    public async Task<CarRentalExternalProviderRentDto> AcceptOffer(
        CarRentalExternalProviderCreateRentDto createRentDto, int offerId)
    {
        string url =
            $"{_configuration.GetValue<string>("CarRentalBaseAPIUrl")}/api/rent/create-rent?offerId={offerId}";
        var json = JsonSerializer.Serialize(createRentDto);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(url, content);
        if (!response.IsSuccessStatusCode)
            throw new HttpRequestException("Couldn't create rent using external provider");
        json = await response.Content.ReadAsStringAsync();
        var rentDto = JsonSerializer.Deserialize<CarRentalExternalProviderRentDto>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
        if (rentDto == null)
            throw new HttpRequestException("Couldn't parse body from external provider");
        return rentDto;
    }

    public async Task<Rent.RentStatus> GetRentStatus(int rentId)
    {
        string url =
            $"{_configuration.GetValue<string>("CarRentalBaseAPIUrl")}/api/rent/status?rentId={rentId}";
        var response = await _httpClient.GetAsync(url);
        if (!response.IsSuccessStatusCode)
            throw new HttpRequestException("Couldn't get rent status");
        var json = await response.Content.ReadAsStreamAsync();
        var rentStatusDto = JsonSerializer.Deserialize<CarRentalExternalProviderRentStatusDto>(json,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        if (rentStatusDto == null)
            throw new HttpRequestException("Couldn't parse body from external provider");
        switch (rentStatusDto.Status)
        {
            case "New":
                return Rent.RentStatus.WaitingForConfirmation;
            case "Confirmed":
                return Rent.RentStatus.Started;
            case "Returned":
                return Rent.RentStatus.WaitingForEmployeeApproval;
            case "Finished":
                return Rent.RentStatus.Finished;
            default:
                throw new HttpRequestException("Unknown rent status returned from external provider");
        }
    }

    public async Task StartRentReturn(int rentId)
    {
        string url =
            $"{_configuration.GetValue<string>("CarRentalBaseAPIUrl")}/api/rent/start-return?rentId={rentId}";
        var response = await _httpClient.PutAsync(url, null);
        if (!response.IsSuccessStatusCode)
            throw new HttpRequestException("Couldn't start rent return");
    }
}