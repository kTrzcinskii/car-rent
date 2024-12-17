using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppRental.Model;

namespace AppRental.DTO
{
    public class CarWorkerDTO
    {
        public int CarId { get; set; }
        public int RentId { get; set; }
        public required string Brand { get; set; }
        public required string Model { get; set; }
        public required string Status { get; set; } 
        

        public static CarWorkerDTO FromCar(Car car, int rentId)
        {
            var workerCarDto = new CarWorkerDTO
            {
                CarId = car.Id,
                RentId = rentId,
                Brand = car.Brand,
                Model = car.Model,
                Status = car.Status.ToString(),
            };
            return workerCarDto;
        }
    }
}