using System.Security.Claims;
using AppBrowser.DTOs;
using AppBrowser.Services.Interfaces;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppBrowser.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IJwtService _jwtService;

    public AuthController(IUserService userService, IJwtService jwtService)
    {
        _userService = userService;
        _jwtService = jwtService;
    }

    [HttpPost("google/callback")]
    public async Task<IActionResult> GoogleCallback([FromBody] string idToken)
    {
        var payload = await GoogleJsonWebSignature.ValidateAsync(idToken);
        var email = payload.Email;
        if (email == null)
        {
            return BadRequest("Couldn't get email from payload");
        }
        var user = await _userService.GetUserByEmailAsync(email);
        if (user == null)
        {
            var registrationToken = _jwtService.GenerateRegistrationToken(email);
            return Ok(new { Token = registrationToken, FinishRegistration = true });
        }

        var token = _jwtService.GenerateToken(user);
        return Ok(new { Token = token, FinishRegistration = false });
    }

    [HttpPost("finish-registration")]
    [Authorize(Policy = "RegistrationOnly")]
    public async Task<IActionResult> FinishRegistration([FromBody] CreateUserDTO createUserDto)
    {
        // TODO: change into extension model
        // https://stackoverflow.com/questions/70988115/what-is-the-right-way-to-retrieve-the-email-from-the-jwt-token
        var email = HttpContext?.User?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
        if (email == null)
        {
            return BadRequest("Couldn't get email from payload");
        }

        if (await _userService.GetUserByEmailAsync(email) != null)
        {
            return BadRequest("User with such email already exists");
        }
        var user = await _userService.CreateUser(createUserDto);
        var token = _jwtService.GenerateToken(user);
        return Ok(new { Token = token, FinishRegistration = false });
    }
    
}