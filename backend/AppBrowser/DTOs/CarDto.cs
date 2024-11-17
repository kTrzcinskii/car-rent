namespace AppBrowser.DTOs;

public class CarDto
{
    public int CarId { get; set; }
    public int ProviderId { get; set; }
    public string BrandName { get; set; }
    public string ModelName { get; set; }
    public int ProductionYear { get; set; }
    public string? ImageUrl { get; set; }
    public string Localization { get; set; }

    public static CarDto MapFromCarRentalProvider(CarRentalProviderCarDto carRentalProviderCarDto, int providerId)
    {
        return new CarDto
        {
            CarId = carRentalProviderCarDto.Id,
            ProviderId = providerId, 
            BrandName = carRentalProviderCarDto.Brand,
            ModelName = carRentalProviderCarDto.Model,
            Localization = carRentalProviderCarDto.Localization,
            ProductionYear = carRentalProviderCarDto.ProductionYear,
        };
    }
}