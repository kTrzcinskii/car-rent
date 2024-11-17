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
        return cars == null ? [] : cars.Select((c) => CarDto.MapFromCarRentalProvider(c, GetProviderId())).ToList();
    }
}