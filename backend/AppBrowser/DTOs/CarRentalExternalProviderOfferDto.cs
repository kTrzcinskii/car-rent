namespace AppBrowser.DTOs;

public class CarRentalExternalProviderOfferDto
{
    public int Id { get; set; }
    public int CarId { get; set; }
    public decimal CostPerDay { get; set; }
    public decimal InsuranceCostPerDay { get; set; }
}