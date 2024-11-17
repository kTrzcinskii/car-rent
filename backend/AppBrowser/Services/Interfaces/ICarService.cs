using AppBrowser.DTOs;

namespace AppBrowser.Services.Interfaces;

public interface ICarService
{ 
    Task<List<CarDto>> SearchCars(string brandName, string modelName);
}