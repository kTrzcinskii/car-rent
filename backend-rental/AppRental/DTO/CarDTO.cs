using AppRental.Model;

namespace AppRental.DTO
{
    public class CarDTO
    {
        public required string Brand { get; set; }
        public required string Model { get; set; }
        public int ProductionYear { get; set; }
        public required string Localization { get; set; }
        public int Id { get; set; }

        public static CarDTO FromCar(Car car)
        {
            var carDto = new CarDTO
            {
                Brand = car.Brand,
                Model = car.Model,
                ProductionYear = car.ProductionYear,
                Id = car.Id,
                Localization = car.Location
            };
            return carDto;
        }
    }
}