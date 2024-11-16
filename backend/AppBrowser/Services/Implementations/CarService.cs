using AppBrowser.DTOs;
using AppBrowser.Services.Interfaces;

namespace AppBrowser.Services.Implementations;

public class CarService : ICarService
{
    public Task<List<CarDto>> SearchCars(string brandName, string modelName)
    {
        throw new NotImplementedException();
    }
}