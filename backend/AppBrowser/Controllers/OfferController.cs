using AppBrowser.DTOs;
using AppBrowser.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppBrowser.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OfferController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IOfferService _offerService;

    public OfferController(IOfferService offerService, IUserService userService)
    {
        _offerService = offerService;
        _userService = userService;
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<OfferDto>> GetOffer([FromQuery] int carId, [FromQuery] int providerId)
    {
        var userInfo = _userService.GetUserInfoFromClaims(HttpContext.User.Claims);
        var user = await _userService.GetUserByEmailAsync(userInfo.Email);
        if (user == null)
        {
            return BadRequest("Couldnt load user info");
        }
        var offerDto = await _offerService.GetOffer(user, carId, providerId);
        return Ok(offerDto);
    }

    [HttpPost("accept")]
    [Authorize]
    public async Task<IActionResult> AcceptOffer([FromQuery] int offerId, [FromQuery] int providerId)
    {
        var userInfo = _userService.GetUserInfoFromClaims(HttpContext.User.Claims);
        var user = await _userService.GetUserByEmailAsync(userInfo.Email);
        if (user == null)
        {
            return BadRequest("Couldnt load user info");
        }

        await _offerService.AcceptOffer(user, offerId, providerId);
        return Ok();
    }
}