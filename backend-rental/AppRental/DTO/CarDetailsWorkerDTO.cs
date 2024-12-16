using AppRental.Model;

namespace AppRental.DTO
{
    public class CarDetailsWorkerDTO
    {
        public int CarId { get; set; }
        public int RentId { get; set; }
        public required string Brand { get; set; }
        public required string Model { get; set; }
        public int ProductionYear { get; set; }
        public required string Localization { get; set; }
        public decimal CostPerDay { get; set; }
        public decimal InsuranceCostPerDay { get; set; }

        public static CarDetailsWorkerDTO FromCar(Car car, int rentId)
        {
            var carDetailsWorkerDto = new CarDetailsWorkerDTO
            {
                CarId = car.Id,
                RentId = rentId,
                Brand = car.Brand,
                Model = car.Model,
                ProductionYear = car.ProductionYear,
                Localization = car.Location,
                CostPerDay = car.CostPerDay,
                InsuranceCostPerDay = car.InsuranceCostPerDay
            };
            return carDetailsWorkerDto;
        }
    }
}