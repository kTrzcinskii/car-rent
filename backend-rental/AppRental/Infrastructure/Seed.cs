using AppRental.Model;
using Microsoft.AspNetCore.Identity;

namespace AppRental.Infrastructure
{
    public class Seed
    {
        public static async Task SeedData(DataContext context, UserManager<IdentityUser> userManager)
        {
            if(!userManager.Users.Any())
            {
                var workers = new List<IdentityUser>
                {
                    new IdentityUser{UserName = "worker1"},
                    new IdentityUser{UserName = "worker2"},
                    new IdentityUser{UserName = "worker3"},
                };
                foreach(var worker in workers)
                {
                    await userManager.CreateAsync(worker, "Pa$$w0rd");
                }
            }
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
                    InsuranceCostPerDay = 20,
                    ImageUrl = "https://carrentalblock.blob.core.windows.net/cars/Skoda-Octavia-2013-thb.jpg"
                },
                new Car
                {
                    Brand = "Skoda",
                    Model = "Octavia",
                    ProductionYear = 2017,
                    Status = CarStatus.Available,
                    Location = "Warsaw",
                    CostPerDay = 95,
                    InsuranceCostPerDay = 25,
                    ImageUrl = "https://carrentalblock.blob.core.windows.net/cars/Skoda-Octavia-2017-thb.jpg"
                },
                new Car
                {
                    Brand = "Skoda",
                    Model = "Octavia",
                    ProductionYear = 2017,
                    Status = CarStatus.Available,
                    Location = "Krakow",
                    CostPerDay = 99,
                    InsuranceCostPerDay = 30,
                    ImageUrl = "https://carrentalblock.blob.core.windows.net/cars/Skoda-Octavia-2017-thb.jpg"
                },
                new Car
                {
                    Brand = "Skoda",
                    Model = "Superb",
                    ProductionYear = 2016,
                    Status = CarStatus.Rented,
                    Location = "Warsaw",
                    CostPerDay = 85,
                    InsuranceCostPerDay = 25,
                    ImageUrl = "https://carrentalblock.blob.core.windows.net/cars/Skoda-Superb-2016-thb.jpg"
                },
                new Car
                {
                    Brand = "Skoda",
                    Model = "Superb",
                    ProductionYear = 2018,
                    Status = CarStatus.Returned,
                    Location = "Warsaw",
                    CostPerDay = 85,
                    InsuranceCostPerDay = 25,
                    ImageUrl = "https://carrentalblock.blob.core.windows.net/cars/Skoda-Superb-2016-thb.jpg"
                },
                new Car
                {
                    Brand = "Skoda",
                    Model = "Superb",
                    ProductionYear = 2016,
                    Status = CarStatus.Rented,
                    Location = "Krakow",
                    CostPerDay = 85,
                    InsuranceCostPerDay = 25,
                    ImageUrl = "https://carrentalblock.blob.core.windows.net/cars/Skoda-Superb-2016-thb.jpg"
                },
                new Car
                {
                    Brand = "Skoda",
                    Model = "Superb",
                    ProductionYear = 2018,
                    Status = CarStatus.Returned,
                    Location = "Krakow",
                    CostPerDay = 85,
                    InsuranceCostPerDay = 25,
                    ImageUrl = "https://carrentalblock.blob.core.windows.net/cars/Skoda-Superb-2016-thb.jpg"
                },
                new Car
                {
                    Brand = "Toyota",
                    Model = "Camry",
                    ProductionYear = 2014,
                    Status = CarStatus.Available,
                    Location = "Warsaw",
                    CostPerDay = 75,
                    InsuranceCostPerDay = 10,
                    ImageUrl = "https://carrentalblock.blob.core.windows.net/cars/Toyota-Camry-2012-thb.jpg"
                },
                new Car
                {
                    Brand = "Toyota",
                    Model = "Yaris",
                    ProductionYear = 2018,
                    Status = CarStatus.Available,
                    Location = "Krakow",
                    CostPerDay = 69,
                    InsuranceCostPerDay = 10,
                    ImageUrl = "https://carrentalblock.blob.core.windows.net/cars/Toyota-Yaris-2017-thb.jpg"
                },
                new Car
                {
                    Brand = "Toyota",
                    Model = "Rav4",
                    ProductionYear = 2018,
                    Status = CarStatus.Rented,
                    Location = "Warsaw",
                    CostPerDay = 109,
                    InsuranceCostPerDay = 30,
                    ImageUrl = "https://carrentalblock.blob.core.windows.net/cars/Toyota-RAV4_Hybrid_EU-Version-2016-thb.jpg"
                },
                new Car
                {
                    Brand = "Hyundai",
                    Model = "i20",
                    ProductionYear = 2013,
                    Status = CarStatus.Available,
                    Location = "Warsaw",
                    CostPerDay = 65,
                    InsuranceCostPerDay = 15,
                    ImageUrl = "https://carrentalblock.blob.core.windows.net/cars/Hyundai-i20-2013-thb.jpg"
                },
                new Car
                {
                    Brand = "Hyundai",
                    Model = "i20",
                    ProductionYear = 2018,
                    Status = CarStatus.Available,
                    Location = "Warsaw",
                    CostPerDay = 70,
                    InsuranceCostPerDay = 15,
                    ImageUrl = "https://carrentalblock.blob.core.windows.net/cars/Hyundai-i20-2015-thb.jpg"
                },
                new Car
                {
                    Brand = "Hyundai",
                    Model = "i20",
                    ProductionYear = 2021,
                    Status = CarStatus.Available,
                    Location = "Warsaw",
                    CostPerDay = 80,
                    InsuranceCostPerDay = 15,
                    ImageUrl = "https://carrentalblock.blob.core.windows.net/cars/Hyundai-i20-2021-thb.jpg"
                },
                new Car
                {
                    Brand = "Toyota",
                    Model = "Corolla",
                    ProductionYear = 2018,
                    Status = CarStatus.Available,
                    Location = "Szczecin",
                    CostPerDay = 99,
                    InsuranceCostPerDay = 20,
                    ImageUrl = "https://carrentalblock.blob.core.windows.net/cars/Toyota-Corolla_EU-Version-2014-thb.jpg"
                },
                new Car
                {
                    Brand = "Ford",
                    Model = "F series",
                    ProductionYear = 2018,
                    Status = CarStatus.Available,
                    Location = "Szczecin",
                    CostPerDay = 99,
                    InsuranceCostPerDay = 20,
                    ImageUrl = "https://carrentalblock.blob.core.windows.net/cars/Ford-F-Series_Super_Duty-2017-thb.jpg"
                },
                new Car
                {
                    Brand = "Honda",
                    Model = "Civic",
                    ProductionYear = 2018,
                    Status = CarStatus.Available,
                    Location = "Szczecin",
                    CostPerDay = 99,
                    InsuranceCostPerDay = 20,
                    ImageUrl = "https://carrentalblock.blob.core.windows.net/cars/Honda-Civic_Sedan-2016-thb.jpg"
                },
                new Car
                {
                    Brand = "Volkswagen",
                    Model = "Tiguan",
                    ProductionYear = 2018,
                    Status = CarStatus.Available,
                    Location = "Szczecin",
                    CostPerDay = 99,
                    InsuranceCostPerDay = 20,
                    ImageUrl = "https://carrentalblock.blob.core.windows.net/cars/Volkswagen-Tiguan-2017-thb.jpg"
                },
                new Car
                {
                    Brand = "Volkswagen",
                    Model = "Golf",
                    ProductionYear = 2018,
                    Status = CarStatus.Available,
                    Location = "Szczecin",
                    CostPerDay = 99,
                    InsuranceCostPerDay = 20,
                    ImageUrl = "https://carrentalblock.blob.core.windows.net/cars/Volkswagen-Golf-2017-thb.jpg"
                },
                new Car
                {
                    Brand = "Honda",
                    Model = "CR-V",
                    ProductionYear = 2018,
                    Status = CarStatus.Available,
                    Location = "Szczecin",
                    CostPerDay = 99,
                    InsuranceCostPerDay = 20,
                    ImageUrl = "https://carrentalblock.blob.core.windows.net/cars/Honda-CR-V-2017-thb.jpg"
                },
                new Car
                {
                    Brand = "Volkswagen",
                    Model = "Polo",
                    ProductionYear = 2018,
                    Status = CarStatus.Available,
                    Location = "Szczecin",
                    CostPerDay = 99,
                    InsuranceCostPerDay = 20,
                    ImageUrl = "https://carrentalblock.blob.core.windows.net/cars/Volkswagen-Polo-2018-thb.jpg"
                },
                new Car
                {
                    Brand = "Toyota",
                    Model = "Camry",
                    ProductionYear = 2018,
                    Status = CarStatus.Available,
                    Location = "Szczecin",
                    CostPerDay = 99,
                    InsuranceCostPerDay = 20,
                    ImageUrl = "https://carrentalblock.blob.core.windows.net/cars/Toyota-Camry-2018-thb.jpg"
                },
                new Car
                {
                    Brand = "Chevrolet",
                    Model = "Silverado",
                    ProductionYear = 2018,
                    Status = CarStatus.Available,
                    Location = "Szczecin",
                    CostPerDay = 99,
                    InsuranceCostPerDay = 20,
                    ImageUrl = "https://carrentalblock.blob.core.windows.net/cars/Chevrolet-Silverado-2016-thb.jpg"
                },
                new Car
                {
                    Brand = "Opel",
                    Model = "Astra",
                    ProductionYear = 2018,
                    Status = CarStatus.Available,
                    Location = "Szczecin",
                    CostPerDay = 99,
                    InsuranceCostPerDay = 20,
                    ImageUrl = "https://carrentalblock.blob.core.windows.net/cars/Opel-Astra-2016-thb.jpg"
                },
                new Car
                {
                    Brand = "Mazda",
                    Model = "3",
                    ProductionYear = 2018,
                    Status = CarStatus.Available,
                    Location = "Szczecin",
                    CostPerDay = 99,
                    InsuranceCostPerDay = 20,
                    ImageUrl = "https://carrentalblock.blob.core.windows.net/cars/Mazda-3-2017-thb.jpg"
                }
            };

            await context.Cars.AddRangeAsync(cars);
            await context.SaveChangesAsync();
        }
    }
}
