using AppRental.Infrastructure;
using AppRental.Model;
using AppRental.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AppRental.Services.Implementations;

public class CarService : ICarService
{
    private readonly DataContext _context;

    public CarService(DataContext context)
    {
        _context = context;
    }

    public async Task<Car?> GetByIdAsync(int id)
    {
        var car = await _context.Cars.FindAsync(id);
        return car;
    }

    public async Task<List<Car>> GetAllCarsAsync()
    {
        var cars = await _context.Cars.Where(car => car.Status == CarStatus.Available).ToListAsync();
        return cars;
    }
}