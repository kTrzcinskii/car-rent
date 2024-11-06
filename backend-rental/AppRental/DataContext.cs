using Microsoft.EntityFrameworkCore;
namespace AppRental
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }
    }
}