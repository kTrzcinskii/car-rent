using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppRental.Model;

namespace AppRental.DTO
{
    public class CarWorkerDTO
    {
        public required string Brand { get; set; }
        public required string Model { get; set; }
        public required string Status { get; set; } 
        public int Id { get; set; }

        public static CarWorkerDTO FromCar(Car car)
        {
            var workerCarDto = new CarWorkerDTO
            {
                Brand = car.Brand,
                Model = car.Model,
                Status = car.Status.ToString(),
                Id = car.Id
            };
            return workerCarDto;
        }
    }
}