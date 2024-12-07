using AppRental.DTO;
using AppRental.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AppRental.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarsController : ControllerBase
    {
        private readonly ICarService _carService;
        
        public CarsController(ICarService carService)
        {
            _carService = carService;
        }

        [HttpGet]
        public async Task<ActionResult<List<CarDTO>>> GetCars()
        {
            var cars = await _carService.GetAllCarsAsync();
            var carDtos = cars.Select(CarDTO.FromCar).ToList();
            return Ok(carDtos);
        }
    }
}