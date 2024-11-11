namespace AppRental.Model
{
    public class Offer
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public decimal CostPerDay { get; set; }
        public decimal InsurancePerDay { get; set; }
    }
}