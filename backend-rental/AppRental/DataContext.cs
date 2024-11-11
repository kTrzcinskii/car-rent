using AppRental.Model;
using Microsoft.EntityFrameworkCore;
namespace AppRental
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Car> Cars { get; set;}
        public DbSet<Offer> Offers { get; set;}
        public DbSet<Rent> Rents { get; set;}
    }
}