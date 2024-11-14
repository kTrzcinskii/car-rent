using AppRental.Model;

namespace AppRental.Infrastructure
{
    public class Seed
    {
        public static async Task SeedData(DataContext context)
        {
            if (context.Cars.Any()) return;

            var cars = new List<Car>
            {
                new Car
                {
                    Brand = "Skoda",
                    Model = "Octavia",
                    ProductionYear = 2013,
                    Status = CarStatus.Available,
                    Location = "Warsaw",
                    CostPerDay = 89,
                },
                new Car
                {
                    Brand = "Skoda",
                    Model = "Octavia",
                    ProductionYear = 2017,
                    Status = CarStatus.Available,
                    Location = "Warsaw",
                    CostPerDay = 95,
                },
                new Car
                {
                    Brand = "Skoda",
                    Model = "Octavia",
                    ProductionYear = 2017,
                    Status = CarStatus.Available,
                    Location = "Krakow",
                    CostPerDay = 99,
                },
                new Car
                {
                    Brand = "Skoda",
                    Model = "Superb",
                    ProductionYear = 2016,
                    Status = CarStatus.Rented,
                    Location = "Warsaw",
                    CostPerDay = 85,
                },
                new Car
                {
                    Brand = "Skoda",
                    Model = "Superb",
                    ProductionYear = 2018,
                    Status = CarStatus.Returned,
                    Location = "Warsaw",
                    CostPerDay = 85,
                },
                new Car
                {
                    Brand = "Skoda",
                    Model = "Superb",
                    ProductionYear = 2016,
                    Status = CarStatus.Rented,
                    Location = "Krakow",
                    CostPerDay = 85,
                },
                new Car
                {
                    Brand = "Skoda",
                    Model = "Superb",
                    ProductionYear = 2018,
                    Status = CarStatus.Returned,
                    Location = "Krakow",
                    CostPerDay = 85,
                },
                new Car
                {
                    Brand = "Toyota",
                    Model = "Camry",
                    ProductionYear = 2014,
                    Status = CarStatus.Available,
                    Location = "Warsaw",
                    CostPerDay = 75,
                },
                new Car
                {
                    Brand = "Toyota",
                    Model = "Yaris",
                    ProductionYear = 2018,
                    Status = CarStatus.Available,
                    Location = "Krakow",
                    CostPerDay = 69,
                },
                new Car
                {
                    Brand = "Toyota",
                    Model = "Rav4",
                    ProductionYear = 2018,
                    Status = CarStatus.Rented,
                    Location = "Warsaw",
                    CostPerDay = 109,
                },
                new Car
                {
                    Brand = "Hyundai",
                    Model = "i20",
                    ProductionYear = 2013,
                    Status = CarStatus.Available,
                    Location = "Warsaw",
                    CostPerDay = 65,
                },
                new Car
                {
                    Brand = "Hyundai",
                    Model = "i20",
                    ProductionYear = 2018,
                    Status = CarStatus.Available,
                    Location = "Warsaw",
                    CostPerDay = 70,
                },
                new Car
                {
                    Brand = "Hyundai",
                    Model = "i20",
                    ProductionYear = 2021,
                    Status = CarStatus.Available,
                    Location = "Warsaw",
                    CostPerDay = 80,
                },
                new Car
                {
                    Brand = "Toyota",
                    Model = "Corolla",
                    ProductionYear = 2018,
                    Status = CarStatus.Available,
                    Location = "Szczecin",
                    CostPerDay = 99,
                },
                new Car
                {
                    Brand = "Ford",
                    Model = "F series",
                    ProductionYear = 2018,
                    Status = CarStatus.Available,
                    Location = "Szczecin",
                    CostPerDay = 99,
                },
                new Car
                {
                    Brand = "Honda",
                    Model = "Civic",
                    ProductionYear = 2018,
                    Status = CarStatus.Available,
                    Location = "Szczecin",
                    CostPerDay = 99,
                },
                new Car
                {
                    Brand = "Volkswagen",
                    Model = "Tiguan",
                    ProductionYear = 2018,
                    Status = CarStatus.Available,
                    Location = "Szczecin",
                    CostPerDay = 99,
                },
                new Car
                {
                    Brand = "Volkswagen",
                    Model = "Golf",
                    ProductionYear = 2018,
                    Status = CarStatus.Available,
                    Location = "Szczecin",
                    CostPerDay = 99,
                },
                new Car
                {
                    Brand = "Honda",
                    Model = "CR-V",
                    ProductionYear = 2018,
                    Status = CarStatus.Available,
                    Location = "Szczecin",
                    CostPerDay = 99,
                },
                new Car
                {
                    Brand = "Volkswagen",
                    Model = "Polo",
                    ProductionYear = 2018,
                    Status = CarStatus.Available,
                    Location = "Szczecin",
                    CostPerDay = 99,
                },
                new Car
                {
                    Brand = "Toyota",
                    Model = "Camry",
                    ProductionYear = 2018,
                    Status = CarStatus.Available,
                    Location = "Szczecin",
                    CostPerDay = 99,
                },
                new Car
                {
                    Brand = "Chevrolet",
                    Model = "Silverado",
                    ProductionYear = 2018,
                    Status = CarStatus.Available,
                    Location = "Szczecin",
                    CostPerDay = 99,
                },
                new Car
                {
                    Brand = "Opel",
                    Model = "Astra",
                    ProductionYear = 2018,
                    Status = CarStatus.Available,
                    Location = "Szczecin",
                    CostPerDay = 99,
                },
                new Car
                {
                    Brand = "Mazda",
                    Model = "3",
                    ProductionYear = 2018,
                    Status = CarStatus.Available,
                    Location = "Szczecin",
                    CostPerDay = 99,
                }

            };

            await context.Cars.AddRangeAsync(cars);
            await context.SaveChangesAsync();
        }
    }
}