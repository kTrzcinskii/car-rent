using AppBrowser.DTOs;
using AppBrowser.Infrastructure;
using AppBrowser.Model;
using AppBrowser.Services.Interfaces;

namespace AppBrowser.Services.Implementations;

public class CarService : ICarService
{
    private readonly DataContext _context;
    private readonly CarRentalExternalProviderService _carRentalExternalProviderService;

    public CarService(CarRentalExternalProviderService carRentalExternalProviderService, DataContext context)
    {
        _carRentalExternalProviderService = carRentalExternalProviderService;
        _context = context;
    }

    public async Task<Car?> GetByIdAsync(int id)
    {
        var car = await _context.Cars.FindAsync(id);
        return car;
    }

    public async Task<List<CarDto>> SearchCars(string brandName, string modelName)
    {
        var carRentalApiCars = await _carRentalExternalProviderService.SearchCars(brandName, modelName);
        // TODO: when we have more than one provider we should somehow merge results here
        return carRentalApiCars.Select(CarDto.FromCar).ToList();
    }
}