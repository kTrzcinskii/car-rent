using Microsoft.AspNetCore.Mvc;

namespace App2.Controllers
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
                return Ok("Connected to the database-rental.");
            }
            else
            {
                return StatusCode(500, "Connection to the database-rental failed.");
            }
        }
    }
}