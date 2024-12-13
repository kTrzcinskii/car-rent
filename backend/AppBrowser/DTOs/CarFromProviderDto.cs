namespace AppBrowser.DTOs;

public class CarFromProviderDto
{
    public int CarId { get; set; }
    public int ProviderId { get; set; }
    public string BrandName { get; set; }
    public string ModelName { get; set; }
    public int ProductionYear { get; set; }
    public string? ImageUrl { get; set; }
    public string Localization { get; set; }

    public static CarFromProviderDto MapFromCarRentalProvider(CarRentalProviderCarDto carRentalProviderCarDto, int providerId)
    {
        return new CarFromProviderDto
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