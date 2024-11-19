using AppRental.Model;

namespace AppRental.DTO
{
    public class OfferDTO
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public decimal CostPerDay { get; set; }
        public decimal InsuranceCostPerDay { get; set; }
    }
}