namespace AppRental.Model
{
    public class Rent
    {
        public int Id { get; set; }
        public virtual required Offer Offer { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public RentStatus Status { get; set; } = RentStatus.New; 

        public List<string>? PhotoUrls { get; set; }
        
        public string? ReturnDescription { get; set; }
        public int WorkerId { get; set; }

        public string? WorkerNote { get; set; }
    }

    public enum RentStatus
    {
        New, // Waiting for confirmation
        Confirmed,
        Returned, //  Waiting for employee approval
        Finished
    }
}