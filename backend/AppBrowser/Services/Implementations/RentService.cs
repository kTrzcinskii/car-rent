using AppBrowser.DTOs;
using AppBrowser.Infrastructure;
using AppBrowser.Model;
using AppBrowser.Services.Interfaces;

namespace AppBrowser.Services.Implementations;

public class RentService : IRentService
{
    private readonly CarRentalExternalProviderService _carRentalExternalProviderService;
    private readonly DataContext _context;
    
    public RentService(CarRentalExternalProviderService carRentalExternalProviderService, DataContext context)
    {
        _carRentalExternalProviderService = carRentalExternalProviderService;
        _context = context;
    }


    public async Task<List<RentDto>> FindUserRents(User user)
    {
        foreach (var rent in user.Rents)
        {
            if (rent.Status == Rent.RentStatus.Finished)
            {
                // For now we assume that status can never be changed from finished to any other state
                continue;
            }
            Rent.RentStatus currentStatus;
            if (rent.ProviderId == _carRentalExternalProviderService.GetProviderId())
            {
                currentStatus = await _carRentalExternalProviderService.GetRentStatus(rent.ExternalRentId);
            }
            else
            {
                throw new ArgumentException("Unknown provider id");
            }
            rent.Status = currentStatus;
        }
        await _context.SaveChangesAsync();
        
        return user.Rents.Select(RentDto.FromRent).ToList();
    }
}