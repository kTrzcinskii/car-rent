using Microsoft.EntityFrameworkCore;

namespace AppBrowser
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }
    }
}