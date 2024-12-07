using AppRental.Model;

namespace AppRental.DTO
{
    public class OfferDTO
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public decimal CostPerDay { get; set; }
        public decimal InsuranceCostPerDay { get; set; }

        public static OfferDTO FromOffer(Offer offer)
        {
            var offerDto = new OfferDTO
            {
                Id = offer.Id,
                CarId = offer.Car.Id,
                CostPerDay = offer.Car.CostPerDay,
                InsuranceCostPerDay = offer.Car.InsuranceCostPerDay
            };
            return offerDto;
        }
    }
}