using Microsoft.AspNetCore.Mvc;

namespace App.Controllers
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
    }
}