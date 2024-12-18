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
                    InsuranceCostPerDay = 20
                },
                new Car
                {
                    Brand = "Skoda",
                    Model = "Octavia",
                    ProductionYear = 2017,
                    Status = CarStatus.Available,
                    Location = "Warsaw",
                    CostPerDay = 95,
                    InsuranceCostPerDay = 25
                },
                new Car
                {
                    Brand = "Skoda",
                    Model = "Octavia",
                    ProductionYear = 2017,
                    Status = CarStatus.Available,
                    Location = "Krakow",
                    CostPerDay = 99,
                    InsuranceCostPerDay = 30
                },
                new Car
                {
                    Brand = "Skoda",
                    Model = "Superb",
                    ProductionYear = 2016,
                    Status = CarStatus.Rented,
                    Location = "Warsaw",
                    CostPerDay = 85,
                    InsuranceCostPerDay = 25
                },
                new Car
                {
                    Brand = "Skoda",
                    Model = "Superb",
                    ProductionYear = 2018,
                    Status = CarStatus.Returned,
                    Location = "Warsaw",
                    CostPerDay = 85,
                    InsuranceCostPerDay = 25
                },
                new Car
                {
                    Brand = "Skoda",
                    Model = "Superb",
                    ProductionYear = 2016,
                    Status = CarStatus.Rented,
                    Location = "Krakow",
                    CostPerDay = 85,
                    InsuranceCostPerDay = 25
                },
                new Car
                {
                    Brand = "Skoda",
                    Model = "Superb",
                    ProductionYear = 2018,
                    Status = CarStatus.Returned,
                    Location = "Krakow",
                    CostPerDay = 85,
                    InsuranceCostPerDay = 25
                },
                new Car
                {
                    Brand = "Toyota",
                    Model = "Camry",
                    ProductionYear = 2014,
                    Status = CarStatus.Available,
                    Location = "Warsaw",
                    CostPerDay = 75,
                    InsuranceCostPerDay = 10
                },
                new Car
                {
                    Brand = "Toyota",
                    Model = "Yaris",
                    ProductionYear = 2018,
                    Status = CarStatus.Available,
                    Location = "Krakow",
                    CostPerDay = 69,
                    InsuranceCostPerDay = 10
                },
                new Car
                {
                    Brand = "Toyota",
                    Model = "Rav4",
                    ProductionYear = 2018,
                    Status = CarStatus.Rented,
                    Location = "Warsaw",
                    CostPerDay = 109,
                    InsuranceCostPerDay = 30
                },
                new Car
                {
                    Brand = "Hyundai",
                    Model = "i20",
                    ProductionYear = 2013,
                    Status = CarStatus.Available,
                    Location = "Warsaw",
                    CostPerDay = 65,
                    InsuranceCostPerDay = 15
                },
                new Car
                {
                    Brand = "Hyundai",
                    Model = "i20",
                    ProductionYear = 2018,
                    Status = CarStatus.Available,
                    Location = "Warsaw",
                    CostPerDay = 70,
                    InsuranceCostPerDay = 15
                },
                new Car
                {
                    Brand = "Hyundai",
                    Model = "i20",
                    ProductionYear = 2021,
                    Status = CarStatus.Available,
                    Location = "Warsaw",
                    CostPerDay = 80,
                    InsuranceCostPerDay = 15
                },
                new Car
                {
                    Brand = "Toyota",
                    Model = "Corolla",
                    ProductionYear = 2018,
                    Status = CarStatus.Available,
                    Location = "Szczecin",
                    CostPerDay = 99,
                    InsuranceCostPerDay = 20
                },
                new Car
                {
                    Brand = "Ford",
                    Model = "F series",
                    ProductionYear = 2018,
                    Status = CarStatus.Available,
                    Location = "Szczecin",
                    CostPerDay = 99,
                    InsuranceCostPerDay = 20
                },
                new Car
                {
                    Brand = "Honda",
                    Model = "Civic",
                    ProductionYear = 2018,
                    Status = CarStatus.Available,
                    Location = "Szczecin",
                    CostPerDay = 99,
                    InsuranceCostPerDay = 20
                },
                new Car
                {
                    Brand = "Volkswagen",
                    Model = "Tiguan",
                    ProductionYear = 2018,
                    Status = CarStatus.Available,
                    Location = "Szczecin",
                    CostPerDay = 99,
                    InsuranceCostPerDay = 20
                },
                new Car
                {
                    Brand = "Volkswagen",
                    Model = "Golf",
                    ProductionYear = 2018,
                    Status = CarStatus.Available,
                    Location = "Szczecin",
                    CostPerDay = 99,
                    InsuranceCostPerDay = 20
                },
                new Car
                {
                    Brand = "Honda",
                    Model = "CR-V",
                    ProductionYear = 2018,
                    Status = CarStatus.Available,
                    Location = "Szczecin",
                    CostPerDay = 99,
                    InsuranceCostPerDay = 20
                },
                new Car
                {
                    Brand = "Volkswagen",
                    Model = "Polo",
                    ProductionYear = 2018,
                    Status = CarStatus.Available,
                    Location = "Szczecin",
                    CostPerDay = 99,
                    InsuranceCostPerDay = 20
                },
                new Car
                {
                    Brand = "Toyota",
                    Model = "Camry",
                    ProductionYear = 2018,
                    Status = CarStatus.Available,
                    Location = "Szczecin",
                    CostPerDay = 99,
                    InsuranceCostPerDay = 20
                },
                new Car
                {
                    Brand = "Chevrolet",
                    Model = "Silverado",
                    ProductionYear = 2018,
                    Status = CarStatus.Available,
                    Location = "Szczecin",
                    CostPerDay = 99,
                    InsuranceCostPerDay = 20
                },
                new Car
                {
                    Brand = "Opel",
                    Model = "Astra",
                    ProductionYear = 2018,
                    Status = CarStatus.Available,
                    Location = "Szczecin",
                    CostPerDay = 99,
                    InsuranceCostPerDay = 20
                },
                new Car
                {
                    Brand = "Mazda",
                    Model = "3",
                    ProductionYear = 2018,
                    Status = CarStatus.Available,
                    Location = "Szczecin",
                    CostPerDay = 99,
                    InsuranceCostPerDay = 20
                },
                // TODO: remove this, only for testing imageUrl
                new Car
                {
                    Brand = "Nissan",
                    Model = "Juke",
                    ProductionYear = 2024,
                    Status = CarStatus.Available,
                    Location = "Olsztyn",
                    CostPerDay = 200,
                    InsuranceCostPerDay = 300,
                    ImageUrl = "https://madmobil.pl/wp-content/uploads/2018/07/426220253_Nissan_Juke_z_roku_modelowego_2018_-_personalizacja_nadwozia_w_kolorze.jpg"
                }
            };

            await context.Cars.AddRangeAsync(cars);
            await context.SaveChangesAsync();
        }
    }
}