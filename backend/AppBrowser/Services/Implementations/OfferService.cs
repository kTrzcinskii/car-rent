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
                Age = 18, // TODO: add age to user model
                YearsWithLicense = user.YearsWithLicense
            };
            var carRentalOfferDto = await _carRentalExternalProviderService.GetOffer(request);
            return OfferDto.MapFromCreateRentalExternalProviderOfferDto(carRentalOfferDto, _carRentalExternalProviderService.GetProviderId());
        }
        throw new ArgumentException("Unknown provider");
    }
}