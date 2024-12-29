using AppBrowser.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppBrowser.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly DataContext _context;
        public TestController(DataContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> TestDatabaseConnection()
        {
            if(await _context.Database.CanConnectAsync())
            {
                return Ok("Connected to the database.");
            }
            else
            {
                return StatusCode(500, "Connection to the database failed.");
            }
        }
        
        [HttpGet("auth")]
        [Authorize]
        public async Task<IActionResult> TestAuthorization()
        {
            return Ok(new { Successful = true });
        }
    }
}