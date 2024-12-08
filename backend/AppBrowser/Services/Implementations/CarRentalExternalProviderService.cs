using System.Text;
using System.Text.Json;
using AppBrowser.DTOs;
using AppBrowser.Services.Interfaces;

namespace AppBrowser.Services.Implementations;

// This is a service that will communicate with our own public API
// Name might be changed later
public class CarRentalExternalProviderService : IExternalProviderService
{
    private readonly IConfiguration _configuration;
    private readonly HttpClient _httpClient;

    private const int CarRentalExternalProviderId = 1;
    
    public CarRentalExternalProviderService(IConfiguration configuration, HttpClient httpClient)
    {
        _configuration = configuration;
        _httpClient = httpClient;
    }

    public int GetProviderId()
    {
        return CarRentalExternalProviderId;
    }

    public async Task<List<CarDto>> SearchCars(string brandName, string modelName)
    {
        string url = $"{_configuration.GetValue<string>("CarRentalBaseAPIUrl")!}/api/cars";
        var response = await _httpClient.GetAsync(url);
        
        if (!response.IsSuccessStatusCode) throw new HttpRequestException("Cannot fetch cars from car rental API");
        
        var json = await response.Content.ReadAsStringAsync();
        var cars = JsonSerializer.Deserialize<List<CarRentalProviderCarDto>>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
        if (cars == null)
        {
            return [];
        }
        var carsDto = cars.Select((c) => CarDto.MapFromCarRentalProvider(c, GetProviderId())).ToList();
        if (brandName == "" && modelName == "")
        {
            return carsDto;
        }

        if (brandName == "")
        {
            // filter by model name
            return carsDto.Where(c => c.ModelName.Contains(modelName, StringComparison.CurrentCultureIgnoreCase)).ToList();
        }
        
        if (modelName == "")
        {
            // filter by brand name
            return carsDto.Where(c => c.BrandName.Contains(brandName, StringComparison.InvariantCultureIgnoreCase)).ToList();
        }
        
        // filter by both
        return carsDto.Where(c => c.BrandName.Contains(brandName, StringComparison.InvariantCultureIgnoreCase) && c.ModelName.Contains(modelName, StringComparison.InvariantCultureIgnoreCase)).ToList();
    }

    public async Task<CarRentalExternalProviderOfferDto> GetOffer(CarRentalExternalProviderCreateOfferDto createOfferDto)
    {
        string url = $"{_configuration.GetValue<string>("CarRentalBaseAPIUrl")}/api/offer";
        var json = JsonSerializer.Serialize(createOfferDto);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(url, content);
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
}