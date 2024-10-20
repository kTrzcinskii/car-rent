using Microsoft.EntityFrameworkCore;

namespace App
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }
    }
}