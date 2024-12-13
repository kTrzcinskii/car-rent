using AppBrowser.DTOs;
using AppBrowser.Services.Interfaces;

namespace AppBrowser.Services.Implementations;

public class CarService : ICarService
{
    private readonly CarRentalExternalProviderService _carRentalExternalProviderService;

    public CarService(CarRentalExternalProviderService carRentalExternalProviderService)
    {
        _carRentalExternalProviderService = carRentalExternalProviderService;
    }

    public async Task<List<CarDto>> SearchCars(string brandName, string modelName)
    {
        var carRentalApiCars = await _carRentalExternalProviderService.SearchCars(brandName, modelName);
        // TODO: when we have more than one provider we should somehow merge results here
        return carRentalApiCars.Select(CarDto.FromCar).ToList();
    }
}