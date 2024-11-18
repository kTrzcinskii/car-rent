using AppRental.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace AppRental.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        public TestController(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        [HttpGet]
        public async Task<IActionResult> TestDatabaseConnection()
        {
            if(await _context.Database.CanConnectAsync())
            {
                return Ok("Connected to the database-rental.");
            }
            else
            {
                return StatusCode(500, "Connection to the database-rental failed.");
            }
        }
    }
}