using AppRental.DTO;
using AppRental.Model;

namespace AppRental.Services.Interfaces;

public interface IRentService
{
    Task<Rent?> GetByIdAsync(int id);
    Task<Rent> CreateRentAsync(Offer offer, RentDTO rentDto);

    Task ConfirmRentAsync(Rent rent);
    Task StartReturnAsync(Rent rent);
    Task ConfirmReturnAsync(Rent rent, string workerId);
}