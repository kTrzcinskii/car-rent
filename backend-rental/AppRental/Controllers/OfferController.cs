using AppRental.DTO;
using AppRental.Infrastructure;
using AppRental.Model;
using Microsoft.AspNetCore.Mvc;

namespace AppRental.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OfferController: ControllerBase
    {
        private readonly DataContext _context;

        public OfferController(DataContext context)
        {
            _context = context;
        }

        [HttpPost]
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

        [HttpPost("{offerId}")]
        public async Task<IActionResult> CreateRent(int offerId, RentDTO rentDTO)
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
                StartDate = DateTime.UtcNow
            };
            _context.Rents.Add(rent);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}