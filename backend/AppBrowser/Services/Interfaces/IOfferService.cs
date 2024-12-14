using AppBrowser.DTOs;
using AppBrowser.Model;

namespace AppBrowser.Services.Interfaces;

public interface IOfferService
{
    Task<Offer?> GetByIdAsync(int id);
    OfferDto? FindValidOffer(User user, Car car);
    Task<OfferDto> GetNewOffer(User user, Car car);
    Task AcceptOffer(User user, Offer offer);
}