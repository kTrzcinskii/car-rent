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
        private readonly IConfiguration _configuration;

        private readonly IJwtService _jwtService;
        private readonly IEmailService _emailService;

        public OfferController(DataContext context, IConfiguration configuration, IJwtService jwtService,
            IEmailService emailService)
        {
            _context = context;
            _configuration = configuration;
            _jwtService = jwtService;
            _emailService = emailService;
        }

        [HttpGet]
        public async Task<ActionResult<OfferDTO>> GetOffer(RequestDTO requestDTO)
        {
            var car = await _context.Cars.FindAsync(requestDTO.CarId);

            if (car == null || car.Status != CarStatus.Available)
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
        [HttpPost("rentme/{offerId}")]
        public async Task<ActionResult<int>> CreateRent(int offerId, RentDTO rentDTO)
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

            return Ok(rent.Id);
        }

        [Authorize]
        [HttpPut("confirm")]
        public async Task<IActionResult> ConfirmRent(int rentId)
        {
            var rent = await _context.Rents.FindAsync(rentId);

            if(rent == null)
                return BadRequest("Rent not found");
            if(rent.Confirmed)
                return BadRequest("Rent has already been confirmed.");

            rent.Confirmed = true;
            rent.StartDate = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}