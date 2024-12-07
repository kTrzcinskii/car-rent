using AppRental.DTO;
using AppRental.Model;
using AppRental.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppRental.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OfferController: ControllerBase
    {
        private readonly IJwtService _jwtService;
        private readonly IEmailService _emailService;
        private readonly ILogger<OfferController> _logger;
        private readonly IOfferService _offerService;
        private readonly ICarService _carService;
        private readonly IRentService _rentService;
        
        public OfferController(IJwtService jwtService, IEmailService emailService, ILogger<OfferController> logger, IOfferService offerService, ICarService carService, IRentService rentService)
        {
            _jwtService = jwtService;
            _emailService = emailService;
            _logger = logger;
            _offerService = offerService;
            _carService = carService;
            _rentService = rentService;
        }

        [HttpPost]
        public async Task<ActionResult<OfferDTO>> GetOffer(RequestDTO requestDTO)
        {
            var car = await _carService.GetByIdAsync(requestDTO.CarId);
            if (car is not { Status: CarStatus.Available })
            {
                return NotFound("Car not found or already rented.");
            }

            var offer = await _offerService.CreateOffer(car);
            var offerDto = OfferDTO.FromOffer(offer); 
            return Ok(offerDto);
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
                return BadRequest("Rent not found");
            if(rent.Confirmed)
                return BadRequest("Rent has already been confirmed.");
            
            _logger.LogInformation("Offer id: {}", rent.Offer.Id);
            _logger.LogInformation("Car id: {}", rent.Offer.Car.Id);
            
            await _rentService.ConfirmRent(rent);
            
            return Ok("Rent successfully confirmed. It should appear in your rents history in few minutes.");
        }
    }
}