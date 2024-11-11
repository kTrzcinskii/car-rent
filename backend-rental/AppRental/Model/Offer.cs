namespace AppRental.Model
{
    public class Offer
    {
        public int Id { get; set; }
        public required Car Car { get; set; }
        public decimal CostPerDay { get; set; }
        public decimal InsuranceCostPerDay { get; set; }

        // public int ValidityPeriod { get; set; }  ???
    }
}