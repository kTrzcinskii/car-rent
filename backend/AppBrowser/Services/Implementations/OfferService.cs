using AppBrowser.DTOs;
using AppBrowser.Infrastructure;
using AppBrowser.Model;
using AppBrowser.Services.Interfaces;

namespace AppBrowser.Services.Implementations;

public class OfferService : IOfferService
{
    // TODO: check if this is actually correct value
    private readonly int OFFER_VALIDITY = 1;
    
    private readonly DataContext _context;
    private readonly CarRentalExternalProviderService _carRentalExternalProviderService;

    public OfferService(CarRentalExternalProviderService carRentalExternalProviderService, DataContext context)
    {
        _carRentalExternalProviderService = carRentalExternalProviderService;
        _context = context;
    }

    
    public OfferDto? FindValidOffer(User user, Car car)
    {
        var offer = user.Offers.FirstOrDefault(o => o.Car == car);
        if (offer == null)
        {
            return null;
        }

        var diff = DateTime.UtcNow - offer.ValidUntil;
        if (diff.TotalMinutes > OFFER_VALIDITY)
        {
            return null;
        }
        
        return OfferDto.FromOffer(offer) ;
    }
    
    public async Task<OfferDto> GetNewOffer(User user, Car car)
    {
        Offer offer;
        if (car.ProviderId == _carRentalExternalProviderService.GetProviderId())
        {
            var request = new CarRentalExternalProviderCreateOfferDto
            {
                CarId = car.ExternalCarId,
                Age = user.Age,
                YearsWithLicense = user.YearsWithLicense
            };
            var carRentalOfferDto = await _carRentalExternalProviderService.GetOffer(request);

            offer = new Offer
            {
                Car = car,
                ProviderId = car.ProviderId,
                CostPerDay = carRentalOfferDto.CostPerDay,
                InsuranceCostPerDay = carRentalOfferDto.InsuranceCostPerDay,
                ExternalOfferId = carRentalOfferDto.Id,
                ValidUntil = DateTime.UtcNow.AddMinutes(OFFER_VALIDITY),
            };
        }
        else
        {
            throw new ArgumentException("Unknown provider");
        }
        
        _context.Offers.Add(offer);
        user.Offers.Add(offer);
        await _context.SaveChangesAsync();
        
        return OfferDto.FromOffer(offer);
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