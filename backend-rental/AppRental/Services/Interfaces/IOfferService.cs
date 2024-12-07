using AppRental.DTO;
using AppRental.Model;

namespace AppRental.Services.Interfaces;

public interface IOfferService
{
    Task<Offer?> GetByIdAsync(int id);
    Task<Offer> CreateOffer(Car car);
}