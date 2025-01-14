
using AppRental.Infrastructure;
using AppRental.Model;
using AppRental.Services.Implementations;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;


namespace Tests;

public class CarServiceTests
{
    private readonly CarService _carService;
    private readonly DataContext _context;
    public CarServiceTests()
    {
        // Dependencies
        var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new DataContext(options);

        // SUT
        _carService = new CarService(_context);
    }

    [Fact]
    public async Task CarService_GetByIdAsync_ReturnsCar()
    {
        // Arrange
        int id = 2;
        _context.Cars.Add(new Car { Id = 2, Status = CarStatus.Available, Brand = "", Model = "", Location = ""});
        _context.SaveChanges();

        // Act
        var result = await _carService.GetByIdAsync(id);

        // Assert
        result.Should().BeOfType<Car>();
        result.Id.Should().Be(id);
    }

    [Fact]
    public async Task CarService_GetByIdAsync_ReturnsNull()
    {
        // Arrange
        int id = 100;

        // Act
        var result = await _carService.GetByIdAsync(id);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task CarService_GetAllCarsAsync_ReturnsCars()
    {
        // Arrange
        var cars = new List<Car>()
        {
            new Car { Id = 1, Status = CarStatus.Available, Brand = "", Model = "", Location = ""},
            new Car { Id = 2, Status = CarStatus.Available, Brand = "", Model = "", Location = ""},
            new Car { Id = 3, Status = CarStatus.Returned, Brand = "", Model = "", Location = ""}
        };
        _context.Cars.AddRange(cars);
        _context.SaveChanges();
        
        // Act
        var result = await _carService.GetAllCarsAsync();
        
        // Assert
        result.Should().BeOfType<List<Car>>();
        result.Should().HaveCount(2);
        result.Should().Contain(car => car.Id == 1);
        result.Should().Contain(car => car.Id == 2);
    }
}