using AppBrowser.DTOs;
using AppBrowser.Model;

namespace AppBrowser.Services.Interfaces;

public interface IOfferService
{
    Task<OfferDto> GetOffer(User user, int carId, int providerId);
}