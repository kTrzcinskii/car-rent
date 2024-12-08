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
        private readonly IOfferService _offerService;
        private readonly ICarService _carService;
        
        public OfferController(IOfferService offerService, ICarService carService)
        {
            _offerService = offerService;
            _carService = carService;
        }

        [HttpPost]
        public async Task<ActionResult<OfferDTO>> GetOffer(RequestDTO requestDTO)
        {
            var car = await _carService.GetByIdAsync(requestDTO.CarId);
            if (car == null)
            {
                return NotFound("Car not found");
            }
            if (car.Status != CarStatus.Available)
            {
                return BadRequest("Car already rented.");
            }

            var offer = await _offerService.CreateOffer(car);
            var offerDto = OfferDTO.FromOffer(offer); 
            return Ok(offerDto);
        }
    }
}