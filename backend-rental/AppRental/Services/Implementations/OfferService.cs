using AppRental.DTO;
using AppRental.Infrastructure;
using AppRental.Model;
using AppRental.Services.Interfaces;

namespace AppRental.Services.Implementations;

public class OfferService : IOfferService
{
    private readonly DataContext _context;

    public OfferService(DataContext context)
    {
        _context = context;
    }

    public async Task<Offer?> GetByIdAsync(int id)
    {
        var offer = await _context.Offers.FindAsync(id);
        return offer;
    }

    public async Task<Offer> CreateOffer(Car car)
    {
        var offer = new Offer 
        {
            Car = car,
            CostPerDay = car.CostPerDay,
            InsuranceCostPerDay = car.InsuranceCostPerDay
            // data waznosci oferty
        };

        _context.Offers.Add(offer);
        await _context.SaveChangesAsync();
        
        return offer;
    }
}