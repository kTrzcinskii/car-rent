namespace AppRental.Model
{
    public class Car
    {
        public int Id { get; set; }

        public required string Brand { get; set; }
        public required string Model { get; set; }
        public int ProductionYear { get; set; }

        public CarStatus Status { get; set; } // metadane dostępność
        public required string Location { get; set; }

        public decimal BaseCostPerDay { get; set; }
    }

    public enum CarStatus
    {
        Available,
        Rented,
        Returned // gotowy do zwrotu
    }
}