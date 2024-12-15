using AppBrowser.Model;

namespace AppBrowser.DTOs;

public class RentDto
{
    public int RentId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string Status { get; set; }
    public decimal CostPerDay { get; set; }
    public decimal InsuranceCostPerDay { get; set; }
    public string ModelName { get; set; }
    public string BrandName { get; set; }
    public string Localization { get; set; }
    public int ProductionYear { get; set; }
    public string? ImageUrl { get; set; }

    public static RentDto FromRent(Rent rent)
    {
        return new RentDto
        {
            RentId = rent.RentId,
            StartDate = rent.StartDate,
            EndDate = rent.EndDate,
            Status = rent.Status.ToString(),
            CostPerDay = rent.Offer.CostPerDay,
            InsuranceCostPerDay = rent.Offer.InsuranceCostPerDay,
            ModelName = rent.Offer.Car.ModelName,
            BrandName = rent.Offer.Car.BrandName,
            Localization = rent.Offer.Car.Location,
            ProductionYear = rent.Offer.Car.ProductionYear,
            ImageUrl = rent.Offer.Car.ImageUrl
        };
    }
}