using AppRental.DTO;
using AppRental.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AppRental.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IJwtService _jwtService;
        public AccountController(UserManager<IdentityUser> userManager, IJwtService jwtService)
        {
            _jwtService = jwtService;
            _userManager = userManager;
        }

        [HttpPost("login")]
        public async Task<ActionResult> LoginWorker([FromBody] LoginDTO loginDTO)
        {
            var user = await _userManager.FindByNameAsync(loginDTO.UserName);
            if(user == null) return Unauthorized();

            var result = await _userManager.CheckPasswordAsync(user, loginDTO.Password);
            if(result)
                return Ok(new {Token = _jwtService.GenerateWorkerToken(user)});
                
            return Unauthorized();
        }

        [HttpGet("info")]
        public async Task<ActionResult> WorkerInfo()
        {
            var workerId = User.Claims.FirstOrDefault(c => c.Type == "workerId")?.Value;
            if(workerId == null) return Unauthorized("No workerId in token's Claims.");

            var worker = await _userManager.FindByIdAsync(workerId);
            if (worker == null)
            {
                return Unauthorized();
            }

            return Ok(new { name = worker.UserName });
        }
    }
}