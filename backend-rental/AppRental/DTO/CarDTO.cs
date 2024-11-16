namespace AppRental.DTO
{
    public class CarDTO
    {
        public required string Brand { get; set; }
        public required string Model { get; set; }
        public int ProductionYear { get; set; }
        public required string Localization { get; set; }
        public int Id { get; set; }
    }
}