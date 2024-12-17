using AppRental.DTO;
using AppRental.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppRental.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarsController : ControllerBase
    {
        private readonly ICarService _carService;
        private readonly IRentService _rentService;
        
        public CarsController(ICarService carService, IRentService rentService)
        {
            _carService = carService;
            _rentService = rentService;
        }

        [HttpGet]
        public async Task<ActionResult<List<CarDTO>>> GetCars()
        {
            var cars = await _carService.GetAllCarsAsync();
            var carDtos = cars.Select(CarDTO.FromCar).ToList();
            return Ok(carDtos);
        }

        [Authorize]
        [HttpGet("worker")]
        public async Task<ActionResult<List<CarWorkerDTO>>> GetCarsInUse()
        {
            var workerCarDtos = await _carService.GetAllCarsInUseAsync();
            return Ok(new {data = workerCarDtos});
        }

        [Authorize]
        [HttpGet("worker/details")]
        public async Task<ActionResult> GetCarDetals([FromQuery]int carId)
        {
            var car = await _carService.GetByIdAsync(carId);
            if(car == null) return NotFound();
            if(car.Status != Model.CarStatus.Returned) return BadRequest("Details are only for returned cars.");
            var rent = await _rentService.GetReturnedRentForCar(car.Id);
            if (rent == null)
            {
                return BadRequest("There is no returned rent for provided car");
            }
            return Ok(CarDetailsWorkerDTO.FromCar(car, rent));
        }

    }
}