using AppBrowser.DTOs;
using AppBrowser.Model;
using AppBrowser.Services.Interfaces;

namespace AppBrowser.Services.Implementations;

public class OfferService : IOfferService
{
    private readonly CarRentalExternalProviderService _carRentalExternalProviderService;

    public OfferService(CarRentalExternalProviderService carRentalExternalProviderService)
    {
        _carRentalExternalProviderService = carRentalExternalProviderService;
    }

    public async Task<OfferDto> GetOffer(User user, int carId, int providerId)
    {
        if (providerId == _carRentalExternalProviderService.GetProviderId())
        {
            var request = new CarRentalExternalProviderCreateOfferDto
            {
                CarId = carId,
                Age = user.Age,
                YearsWithLicense = user.YearsWithLicense
            };
            var carRentalOfferDto = await _carRentalExternalProviderService.GetOffer(request);
            return OfferDto.MapFromCreateRentalExternalProviderOfferDto(carRentalOfferDto, _carRentalExternalProviderService.GetProviderId());
        }
        throw new ArgumentException("Unknown provider");
    }

    public async Task AcceptOffer(User user, int offerId, int providerId)
    {
        if (providerId == _carRentalExternalProviderService.GetProviderId())
        {
            var request = new CarRentalExternalProviderCreateRentDto
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
            // TODO: maybe send it back to client?
            // TODO: probably need to save it in browser db later on
            var _ = await _carRentalExternalProviderService.AcceptOffer(request, offerId);
            return;
        }
        throw new ArgumentException("Unknown provider");
    }
}