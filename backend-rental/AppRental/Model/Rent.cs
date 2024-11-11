namespace AppRental.Model
{
    public class Rent
    {
        public int Id { get; set; }
        public required Car Car { get; set; }

        public required string UserId { get; set; } // from auth

        public decimal CostPerDay { get; set; }

        public decimal InsurancePerDay { get; set; }

        public required string Email { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}