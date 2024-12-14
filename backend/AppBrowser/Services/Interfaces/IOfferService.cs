using AppBrowser.DTOs;
using AppBrowser.Model;

namespace AppBrowser.Services.Interfaces;

public interface IOfferService
{
    OfferDto? FindValidOffer(User user, Car car);
    Task<OfferDto> GetNewOffer(User user, Car car);
    Task AcceptOffer(User user, int offerId, int providerId);
}