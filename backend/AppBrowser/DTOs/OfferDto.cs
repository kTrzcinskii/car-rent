namespace AppBrowser.DTOs;

public class OfferDto
{
    public int OfferId { get; set; }
    public int CarId { get; set; }
    public int ProviderId { get; set; }
    public decimal CostPerDay { get; set; }
    public decimal InsuranceCostPerDay { get; set; }

    public static OfferDto MapFromCreateRentalExternalProviderOfferDto(
        CarRentalExternalProviderOfferDto carRentalExternalProviderOfferDto, int providerId)
    {
        return new OfferDto
        {
            CarId = carRentalExternalProviderOfferDto.CarId,
            OfferId = carRentalExternalProviderOfferDto.Id,
            ProviderId = providerId,
            CostPerDay = carRentalExternalProviderOfferDto.CostPerDay,
            InsuranceCostPerDay = carRentalExternalProviderOfferDto.InsuranceCostPerDay
        };
    }
}