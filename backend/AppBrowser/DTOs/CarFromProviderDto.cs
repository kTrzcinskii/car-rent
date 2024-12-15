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

    public static CarFromProviderDto MapFromCarRentalProvider(CarRentalExternalProviderCarDto carRentalExternalProviderCarDto, int providerId)
    {
        return new CarFromProviderDto
        {
            CarId = carRentalExternalProviderCarDto.Id,
            ProviderId = providerId, 
            BrandName = carRentalExternalProviderCarDto.Brand,
            ModelName = carRentalExternalProviderCarDto.Model,
            Localization = carRentalExternalProviderCarDto.Localization,
            ProductionYear = carRentalExternalProviderCarDto.ProductionYear,
        };
    }
}