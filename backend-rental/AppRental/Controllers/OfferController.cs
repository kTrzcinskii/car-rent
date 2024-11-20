using AppRental.DTO;
using AppRental.Infrastructure;
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
        private readonly DataContext _context;
        private readonly IJwtService _jwtService;
        private readonly IEmailService _emailService;
        private readonly ILogger<OfferController> _logger;

        public OfferController(DataContext context, IJwtService jwtService, IEmailService emailService, ILogger<OfferController> logger)
        {
            _context = context;
            _jwtService = jwtService;
            _emailService = emailService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult<OfferDTO>> GetOffer(RequestDTO requestDTO)
        {
            var car = await _context.Cars.FindAsync(requestDTO.CarId);

            if (car is not { Status: CarStatus.Available })
            {
                return NotFound("Car not found or already rented.");
            }

            var offer = new Offer 
            {
                Car = car,
                CostPerDay = car.CostPerDay,
                InsuranceCostPerDay = car.InsuranceCostPerDay
                // data waznosci oferty
            };

            _context.Offers.Add(offer);
            await _context.SaveChangesAsync();

            var offerDTO = new OfferDTO
            {
                Id = offer.Id,
                CarId = car.Id,
                CostPerDay = car.CostPerDay,
                InsuranceCostPerDay = car.InsuranceCostPerDay
            };

            return Ok(offerDTO);
        }
        [HttpPost("create-rent")]
        public async Task<ActionResult> CreateRent([FromQuery] int offerId, [FromBody] RentDTO rentDTO)
        {
            var offer = await _context.Offers.FindAsync(offerId);
            if(offer == null)
            {
                return BadRequest();
            }

            var rent = new Rent
            {
                Offer = offer,
                FirstName = rentDTO.FirstName,
                LastName = rentDTO.LastName,
                Email = rentDTO.Email,
            };
            _context.Rents.Add(rent);
            await _context.SaveChangesAsync();

            var confirmationLink = _jwtService.GenerateLink(rent.Id);
            await _emailService.SendRentConfirmationEmailAsync(rent.Email, confirmationLink);

            return Ok(new {RentId = rent.Id});
        }

        [Authorize]
        [HttpGet("confirm-rent")]
        public async Task<IActionResult> ConfirmRent(int rentId)
        {
            var rent = await _context.Rents.FindAsync(rentId);
            _logger.LogInformation("Rent id: {}", rent?.Id);
            
            if(rent == null)
                return BadRequest("Rent not found");
            if(rent.Confirmed)
                return BadRequest("Rent has already been confirmed.");

            rent.Confirmed = true;
            rent.StartDate = DateTime.UtcNow;

            _logger.LogInformation("Offer id: {}", rent.Offer.Id);
            _logger.LogInformation("Car id: {}", rent.Offer.Car.Id);
            
            var car = _context.Cars.FirstOrDefault((c) => c.Id == rent.Offer.Car.Id);
            
            if (car == null)
                return BadRequest("Car for the rent not found");
            car.Status = CarStatus.Rented;
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}