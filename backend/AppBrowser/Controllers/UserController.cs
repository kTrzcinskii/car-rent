using AppBrowser.DTOs;
using AppBrowser.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppBrowser.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly IUserService _userService;
    
    public UserController(ILogger<UserController> logger, IUserService userService)
    {
        _logger = logger;
        _userService = userService;
    }

    [Authorize]
    [HttpGet("my-info")]
    public ActionResult<UserInfoDto> GetUserInfo()
    {
        var claims = HttpContext.User.Claims;
        var userInfo = _userService.GetUserInfoFromClaims(claims);
        return Ok(userInfo);
    }

}