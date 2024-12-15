using AppRental.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace AppRental.Infrastructure
{
    public class DataContext: IdentityDbContext<IdentityUser>
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Car> Cars { get; set;}
        public DbSet<Offer> Offers { get; set;}
        public DbSet<Rent> Rents { get; set;}
    }
}