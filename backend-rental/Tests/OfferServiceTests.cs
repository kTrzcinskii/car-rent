using AppRental.Infrastructure;
using AppRental.Model;
using AppRental.Services.Implementations;
using Microsoft.EntityFrameworkCore;
using FluentAssertions;

namespace Tests
{
    public class OfferServiceTests
    {
        private readonly OfferService _offerService;
        private readonly DataContext _context;

        public OfferServiceTests()
        {
            // Dependencies
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new DataContext(options);

            // SUT
            _offerService = new OfferService(_context);
        }

        [Fact]
        public async Task OfferService_GetByIdAsync_ReturnsOffer()
        {
            // Arrange
            var car = new Car { Id = 1, Brand = "", Model = "", CostPerDay = 50, InsuranceCostPerDay = 10, Location = "" };
            var offer = new Offer
            {
                Car = car,
                CostPerDay = car.CostPerDay,
                InsuranceCostPerDay = car.InsuranceCostPerDay
            };
            _context.Offers.Add(offer);
            _context.SaveChanges();

            // Act
            var result = await _offerService.GetByIdAsync(offer.Id);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(offer.Id);
            result.Car.Id.Should().Be(car.Id);
        }

        [Fact]
        public async Task OfferService_CreateOffer_CarIsValid()
        {
            // Arrange
            var car = new Car { Id = 1, Brand = "", Model = "", CostPerDay = 50, InsuranceCostPerDay = 10, Location = "" };

            // Act
            var result = await _offerService.CreateOffer(car);

            // Assert
            result.Should().NotBeNull();
            result.Car.Id.Should().Be(car.Id);
            result.CostPerDay.Should().Be(car.CostPerDay);
            result.InsuranceCostPerDay.Should().Be(car.InsuranceCostPerDay);
        }
    }
}