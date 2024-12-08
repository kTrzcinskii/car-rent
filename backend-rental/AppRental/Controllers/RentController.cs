using AppRental.DTO;
using AppRental.Model;
using AppRental.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppRental.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RentController : ControllerBase
{
    private readonly IOfferService _offerService;
    private readonly IRentService _rentService;
    private readonly IJwtService _jwtService;
    private readonly IEmailService _emailService;
    private readonly ILogger<RentController> _logger;

    public RentController(IOfferService offerService, IRentService rentService, IJwtService jwtService, IEmailService emailService, ILogger<RentController> logger)
    {
        _offerService = offerService;
        _rentService = rentService;
        _jwtService = jwtService;
        _emailService = emailService;
        _logger = logger;
    }

    [HttpPost("create-rent")]
    public async Task<ActionResult> CreateRent([FromQuery] int offerId, [FromBody] RentDTO rentDTO)
    {
        var offer = await _offerService.GetByIdAsync(offerId);
        if(offer == null)
        {
            return BadRequest();
        }

        var rent = await _rentService.CreateRent(offer, rentDTO);

        var confirmationLink = _jwtService.GenerateLink(rent.Id);
        await _emailService.SendRentConfirmationEmailAsync(rent.Email, confirmationLink);

        return Ok(new {RentId = rent.Id});
    }

    [Authorize]
    [HttpGet("confirm-rent")]
    public async Task<IActionResult> ConfirmRent(int rentId)
    {
        var rent = await _rentService.GetByIdAsync(rentId);
        _logger.LogInformation("Rent id: {}", rent?.Id);
            
        if(rent == null)
            return NotFound("Rent not found");
        if(rent.Status != RentStatus.New)
            return BadRequest("Rent has already been confirmed.");
            
        _logger.LogInformation("Offer id: {}", rent.Offer.Id);
        _logger.LogInformation("Car id: {}", rent.Offer.Car.Id);
            
        await _rentService.ConfirmRent(rent);
            
        return Ok("Rent successfully confirmed. It should appear in your rents history in few minutes.");
    }

    [HttpGet("status")]
    public async Task<ActionResult<RentStatusDTO>> GetRentStatus([FromQuery] int rentId)
    {
        var rent = await _rentService.GetByIdAsync(rentId);
        if (rent == null)
            return NotFound("Rent not found");
        var rentStatusDto = RentStatusDTO.FromRent(rent);
        return Ok(rentStatusDto);
    }
}