using Microsoft.AspNetCore.Mvc;

namespace AppBrowser.Controllers
{
    // TODO: it seems that azure require application to return something on "/"
    // in the future we should delete this controller and return something meaningful or redirect to 404
    [ApiController]
    [Route("/")]
    public class IndexController : ControllerBase
    {
        private readonly DataContext _context;
        public IndexController(DataContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return Ok("Hello");
        }
    }
}