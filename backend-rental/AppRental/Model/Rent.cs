namespace AppRental.Model
{
    public class Rent
    {
        public int Id { get; set; }
        public required Offer Offer { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public bool Confirmed { get; set; } = false; 
    }
}