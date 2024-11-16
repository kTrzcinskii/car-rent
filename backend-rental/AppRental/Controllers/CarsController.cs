using AppRental.DTO;
using AppRental.Infrastructure;
using AppRental.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppRental.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarsController : ControllerBase
    {
        private readonly DataContext _context;

        public CarsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<CarDTO>>> GetCars()
        {
            var cars = await _context.Cars.Where(car => car.Status == CarStatus.Available).ToListAsync();
            var carDTOs = cars.Select(car => new CarDTO
            {
                Brand = car.Brand,
                Model = car.Model,
                ProductionYear = car.ProductionYear,
                Id = car.Id,
                Localization = car.Location
            }).ToList();

            return Ok(carDTOs);
        }
    }
}