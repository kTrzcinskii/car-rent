using AppBrowser.Model;

namespace AppBrowser.DTOs;

public class OfferDto
{
    public int OfferId { get; set; }
    public decimal CostPerDay { get; set; }
    public decimal InsuranceCostPerDay { get; set; }

    public static OfferDto FromOffer(Offer offer)
    {
        return new OfferDto
        {
            OfferId = offer.OfferId,
            CostPerDay = offer.CostPerDay,
            InsuranceCostPerDay = offer.InsuranceCostPerDay
        };
    }
}