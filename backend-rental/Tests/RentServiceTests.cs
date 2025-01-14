using AppRental.Infrastructure;
using AppRental.Model;
using AppRental.Services.Implementations;
using Microsoft.EntityFrameworkCore;
using FluentAssertions;
using AppRental.DTO;

namespace Tests
{
    public class RentServiceTests
    {
        private readonly RentService _rentService;
        private readonly DataContext _context;

        public RentServiceTests()
        {
            // Dependencies
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new DataContext(options);

            // SUT
            _rentService = new RentService(_context);
        }

        [Fact]
        public async Task RentService_GetByIdAsync_ReturnsRent()
        {
            // Arrange
            var car = new Car { Id = 1, Brand = "", Model = "", Location = ""};
            var offer = new Offer { Car = car, CostPerDay = 50, InsuranceCostPerDay = 10 };
            var rent = new Rent { Offer = offer, FirstName = "", LastName = "", Email = "" };
            _context.Rents.Add(rent);
            _context.SaveChanges();

            // Act
            var result = await _rentService.GetByIdAsync(rent.Id);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(rent.Id);
            result.Offer.Car.Id.Should().Be(car.Id);
        }

        [Fact]
        public async Task RentService_GetReturnedRentForCar_ReturnsRent()
        {
            // Arrange
            var car = new Car { Id = 1, Brand = "", Model = "", Location = "" };
            var offer = new Offer { Car = car };
            var rent = new Rent { Offer = offer, Status = RentStatus.Returned, FirstName = "", LastName = "", Email = "" };
            _context.Rents.Add(rent);
            _context.SaveChanges();

            // Act
            var result = await _rentService.GetReturnedRentForCar(car.Id);

            // Assert
            result.Should().NotBeNull();
            result.Offer.Car.Id.Should().Be(car.Id);
            result.Status.Should().Be(RentStatus.Returned);
        }

        [Fact]
        public async Task RentService_CreateRentAsync_CreatesRent()
        {
            // Arrange
            var car = new Car { Id = 1, Brand = "", Model = "", Location = "" };
            var offer = new Offer { Car = car };
            var rentDto = new RentDTO
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@gmail.com"
            };

            // Act
            var result = await _rentService.CreateRentAsync(offer, rentDto);

            // Assert
            result.Should().NotBeNull();
            result.Offer.Id.Should().Be(offer.Id);
            result.FirstName.Should().Be(rentDto.FirstName);
            result.LastName.Should().Be(rentDto.LastName);
            result.Email.Should().Be(rentDto.Email);
        }

        [Fact]
        public async Task RentService_ConfirmRentAsync_ConfirmsValidRent()
        {
            // Arrange
            var car = new Car { Id = 1, Status = CarStatus.Available, Brand = "", Model = "", Location = "" };
            var offer = new Offer { Car = car };
            var rent = new Rent { Offer = offer, FirstName = "", LastName = "", Email = "" };
            _context.Rents.Add(rent);
            await _context.SaveChangesAsync();

            // Act
            await _rentService.ConfirmRentAsync(rent);

            // Assert
            rent.Status.Should().Be(RentStatus.Confirmed);
            rent.StartDate.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(3));
            car.Status.Should().Be(CarStatus.Rented);
        }

        [Fact]
        public async Task RentService_StartReturnAsync()
        {
            // Arrange
            var car = new Car { Id = 1, Status = CarStatus.Rented, Brand = "", Model = "", Location = "" };
            var offer = new Offer { Car = car };
            var rent = new Rent { Offer = offer, Status = RentStatus.Confirmed, FirstName = "", LastName = "", Email = "" };
            _context.Rents.Add(rent);
            _context.SaveChanges();

            // Act
            await _rentService.StartReturnAsync(rent);

            // Assert
            rent.Status.Should().Be(RentStatus.Returned);
            car.Status.Should().Be(CarStatus.Returned);
        }

        [Fact]
        public async Task RentService_ConfirmReturnAsync()
        {
            // Arrange
            var car = new Car { Id = 1, Status = CarStatus.Returned, Brand = "", Model = "", Location = "" };
            var offer = new Offer { Car = car };
            var rent = new Rent { Offer = offer, Status = RentStatus.Returned, FirstName = "", LastName = "", Email = "" };
            _context.Rents.Add(rent);
            await _context.SaveChangesAsync();

            var workerId = "worker1";

            // Act
            await _rentService.ConfirmReturnAsync(rent, workerId);

            // Assert
            rent.Status.Should().Be(RentStatus.Finished);
            car.Status.Should().Be(CarStatus.Available);
            rent.WorkerId.Should().Be(workerId);
            rent.EndDate.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(3));
        }
    }
}