using AppBrowser.DTOs;
using AppBrowser.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AppBrowser.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RentController : ControllerBase
{
    private readonly IRentService _rentService;
    private readonly IPaginationService _paginationService;
    private readonly IUserService _userService;
    private readonly ILogger<RentController> _logger;
    
    public RentController(IRentService rentService, IPaginationService paginationService, IUserService userService, ILogger<RentController> logger)
    {
        _rentService = rentService;
        _paginationService = paginationService;
        _userService = userService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<PaginatedDto<RentDto>>> GetRents([FromQuery] int page = 0,
        [FromQuery] int pageSize = 5)
    {
        var userInfo = _userService.GetUserInfoFromClaims(HttpContext.User.Claims);
        var user = await _userService.GetUserByEmailAsync(userInfo.Email);
        if (user == null)
        {
            _logger.LogError("Cannot load user info from claims.");
            return BadRequest("Couldnt load user info");
        }
        var rentsDtos = await _rentService.FindUserRents(user);
        var response = _paginationService.GetPaginatedResponse(rentsDtos, page, pageSize);
        return Ok(response);
    }
}