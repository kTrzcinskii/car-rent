using AppRental.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<IdentityUser> _userManager;
        public TestController(DataContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
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