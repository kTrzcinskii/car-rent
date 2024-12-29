using AppBrowser.Model;
using Microsoft.EntityFrameworkCore;

namespace AppBrowser.Infrastructure
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }
        
        public DbSet<User> Users { get; set; }
        public DbSet<Rent> Rents { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Offer> Offers { get; set; }
    }
}