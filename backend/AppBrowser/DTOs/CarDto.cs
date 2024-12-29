using AppBrowser.Model;

namespace AppBrowser.DTOs;

public class CarDto
{
    public int CarId { get; set; }
    public string ModelName { get; set; }
    public string BrandName { get; set; }
    public string Localization { get; set; }
    public int ProductionYear { get; set; }
    public string? ImageUrl { get; set; } = null;

    public static CarDto FromCar(Car car)
    {
        return new CarDto
        {
            CarId = car.CarId,
            ModelName = car.ModelName,
            BrandName = car.BrandName,
            Localization = car.Location,
            ProductionYear = car.ProductionYear,
            ImageUrl = car.ImageUrl
        };
    }
}