using AppBrowser.DTOs;
using AppBrowser.Model;

namespace AppBrowser.Services.Interfaces;

public interface ICarService
{ 
    Task<Car?> GetByIdAsync(int id);
    Task<List<CarDto>> SearchCars(string brandName, string modelName);
}