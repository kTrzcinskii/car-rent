using AppRental.DTO;
using AppRental.Infrastructure;
using AppRental.Model;
using Microsoft.AspNetCore.Mvc;

namespace AppRental.Controllers
{
    public class OfferController: ControllerBase
    {
        private readonly DataContext _context;

        public OfferController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<Offer>> GetOffer(RequestDTO requestDTO)
        {
            var car = await _context.Cars.FindAsync(requestDTO.CarId);

            if (car == null || car.Status != CarStatus.Available)
            {
                return NotFound("Car not found or already rented.");
            }

            var offer = new Offer 
            {
                CarId = requestDTO.CarId,
                CostPerDay = car.CostPerDay,
                InsuranceCostPerDay = car.InsuranceCostPerDay
                // data waznosci oferty
            };

            _context.Offers.Add(offer);
            await _context.SaveChangesAsync();

            return Ok(offer);
        }

        [HttpPost("{offerId}")]
        public async Task<IActionResult> CreateRent(int offerId, RentDTO rentDTO)
        {
            var rent = new Rent
            {
                OfferId = offerId,
                FirstName = rentDTO.FirstName,
                LastName = rentDTO.LastName,
                Email = rentDTO.Email,
                StartDate = DateTime.Now
            };
            _context.Rents.Add(rent);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}