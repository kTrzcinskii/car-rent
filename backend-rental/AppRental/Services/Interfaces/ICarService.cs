using AppRental.Model;

namespace AppRental.Services.Interfaces;

public interface ICarService
{
    Task<Car?> GetByIdAsync(int id);
    Task<List<Car>> GetAllCarsAsync();
}