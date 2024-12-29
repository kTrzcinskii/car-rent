using AppBrowser.DTOs;
using AppBrowser.Infrastructure;
using AppBrowser.Model;
using AppBrowser.Services.Interfaces;

namespace AppBrowser.Services.Implementations;

public class OfferService : IOfferService
{
    // TODO: check if this is actually correct value
    private readonly int OFFER_VALIDITY = 20;
    
    private readonly DataContext _context;
    private readonly CarRentalExternalProviderService _carRentalExternalProviderService;

    public OfferService(CarRentalExternalProviderService carRentalExternalProviderService, DataContext context)
    {
        _carRentalExternalProviderService = carRentalExternalProviderService;
        _context = context;
    }


    public async Task<Offer?> GetByIdAsync(int id)
    {
        var offer = await _context.Offers.FindAsync(id);
        return offer;
    }

    public OfferDto? FindValidOffer(User user, Car car)
    {
        var offer = user.Offers.FirstOrDefault(o => o.Car == car);
        if (offer == null)
        {
            return null;
        }

        if (!IsOfferValid(offer))
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

    public async Task AcceptOffer(User user, Offer offer)
    {
        if (!IsOfferValid(offer))
        {
            throw new BadHttpRequestException("Offer is no longer valid.");
        }
        
        Rent rent;
        if (offer.ProviderId == _carRentalExternalProviderService.GetProviderId())
        {
            var request = new CarRentalExternalProviderCreateRentDto
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
            var carRentalExternalProviderRentDto = await _carRentalExternalProviderService.AcceptOffer(request, offer.ExternalOfferId);

            rent = new Rent
            {
                ProviderId = offer.ProviderId,
                ExternalRentId = carRentalExternalProviderRentDto.RentId,
                StartDate = DateTime.UtcNow,
                Status = Rent.RentStatus.WaitingForConfirmation,
                Offer = offer
            };
        }
        else
        {
            throw new ArgumentException("Unknown provider");
        }

        _context.Rents.Add(rent);
        user.Rents.Add(rent);
        await _context.SaveChangesAsync();
    }

    private bool IsOfferValid(Offer offer)
    {
        var diff = DateTime.UtcNow - offer.ValidUntil;
        return diff.TotalMinutes <= OFFER_VALIDITY;
    }
}