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
    private readonly IPhotoService _photoService;

    public RentController(IOfferService offerService, IRentService rentService, IJwtService jwtService, 
        IEmailService emailService, ILogger<RentController> logger, IPhotoService photoService)
    {
        _photoService = photoService;
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

        var rent = await _rentService.CreateRentAsync(offer, rentDTO);

        var confirmationLink = _jwtService.GenerateLink(rent.Id);
        await _emailService.SendRentConfirmationEmailAsync(rent.Email, confirmationLink);

        return Ok(new {RentId = rent.Id});
    }

    [Authorize(Policy = "EmailLink")]
    [HttpGet("confirm-rent")]
    public async Task<IActionResult> ConfirmRent()
    {
        var rentIdClaim = User.Claims.FirstOrDefault(c => c.Type == "rentId")?.Value;
        if(rentIdClaim == null) return Unauthorized("No rentId in token's Claims.");
        var rentId = Convert.ToInt32(rentIdClaim);
        

        var rent = await _rentService.GetByIdAsync(rentId);
        _logger.LogInformation("Rent id: {}", rent?.Id);
            
        if(rent == null)
            return NotFound("Rent not found");
        if(rent.Status != RentStatus.New)
            return BadRequest("Rent has already been confirmed.");
            
        _logger.LogInformation("Offer id: {}", rent.Offer.Id);
        _logger.LogInformation("Car id: {}", rent.Offer.Car.Id);
            
        await _rentService.ConfirmRentAsync(rent);
            
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

    [HttpPut("start-return")]
    public async Task<ActionResult> StartReturn([FromForm]int rentId, [FromForm]string carStateDescription, List<IFormFile> photos)
    {
        var rent = await _rentService.GetByIdAsync(rentId);
        if(rent == null)
        {
            return NotFound();
        }
        await _rentService.StartReturnAsync(rent, carStateDescription);
        await _photoService.AddPhotosToAzureAsync(rent, photos);

        return Ok();
    }

    [HttpPut("confirm-return")] // worker
    public async Task<ActionResult> ConfirmReturn([FromForm]int rentId, [FromForm]int workerId, List<IFormFile> photos)
    {
        var rent = await _rentService.GetByIdAsync(rentId);

        if(rent == null) return NotFound();
        if(rent.Status != RentStatus.Returned) return BadRequest();

        await _rentService.ConfirmReturnAsync(rent, workerId);
        await _photoService.AddPhotosToAzureAsync(rent, photos);
        await _emailService.SendBillingEmailAsync(rent);

        return Ok();
    }

}