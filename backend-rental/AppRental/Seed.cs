using AppRental.Model;

namespace AppRental
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
                    Brand = "",
                    Model = "",
                    ProductionYear = 2000,
                    Status = CarStatus.Available,
                    Location = "",
                    BaseCostPerDay = 0,
                }
            };

            await context.Cars.AddRangeAsync(cars);
            await context.SaveChangesAsync();
        }
    }
}