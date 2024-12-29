using AppBrowser.DTOs;
using AppBrowser.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AppBrowser.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CarController : ControllerBase
{
    private readonly ICarService _carService;
    private readonly IPaginationService _paginationService;

    public CarController(ICarService carService, IPaginationService paginationService)
    {
        _carService = carService;
        _paginationService = paginationService;
    }

    [HttpGet("search")]
    public async Task<ActionResult<PaginatedDto<CarDto>>> SearchCars([FromQuery] string brandName = "", [FromQuery] string modelName = "", [FromQuery] int page = 0, [FromQuery] int pageSize = 5)
    {
        var allCars = await _carService.SearchCars(brandName, modelName);
        var response = _paginationService.GetPaginatedResponse(allCars, page, pageSize);
        return Ok(response);
    }
}