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
                    Brand = "Brand",
                    Model = "Model",
                    ProductionYear = 2000,
                    Status = CarStatus.Available,
                    Location = "W",
                    CostPerDay = 0,
                }
            };

            await context.Cars.AddRangeAsync(cars);
            await context.SaveChangesAsync();
        }
    }
}