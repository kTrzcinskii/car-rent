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
    }
}