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
    private readonly ICarService _carService;
    private readonly ILogger<OfferController> _logger;

    public OfferController(IOfferService offerService, IUserService userService, ICarService carService, ILogger<OfferController> logger)
    {
        _offerService = offerService;
        _userService = userService;
        _carService = carService;
        _logger = logger;
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<OfferDto>> GetOffer([FromQuery] int carId)
    {
        var userInfo = _userService.GetUserInfoFromClaims(HttpContext.User.Claims);
        var user = await _userService.GetUserByEmailAsync(userInfo.Email);
        if (user == null)
        {
            return BadRequest("Couldnt load user info");
        }
        var car = await _carService.GetByIdAsync(carId);
        if (car == null)
        {
            return NotFound("Car not found");
        }

        var alreadyExistingOffer = _offerService.FindValidOffer(user, car);
        if (alreadyExistingOffer != null)
        {
            _logger.LogInformation("Returning already existing offer.");
            return Ok(alreadyExistingOffer);
        }
        
        var offerDto = await _offerService.GetNewOffer(user, car);
        _logger.LogInformation("Returning new offer.");
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