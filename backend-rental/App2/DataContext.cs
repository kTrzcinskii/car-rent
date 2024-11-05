using Microsoft.EntityFrameworkCore;
namespace App2
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }
    }
}