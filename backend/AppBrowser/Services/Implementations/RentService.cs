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


    public async Task<Rent?> GetByIdAsync(int id)
    {
        var rent = await _context.Rents.FindAsync(id);
        return rent;
    }

    public async Task<List<RentDto>> FindUserRents(User user)
    {
        foreach (var rent in user.Rents)
        {
            if (rent.ProviderId == _carRentalExternalProviderService.GetProviderId())
            {
                var rentStatusDto = await _carRentalExternalProviderService.GetRentStatus(rent.ExternalRentId);
                Rent.RentStatus status = rentStatusDto.Status switch
                {
                    "New" => Rent.RentStatus.WaitingForConfirmation,
                    "Confirmed" => Rent.RentStatus.Started,
                    "Returned" => Rent.RentStatus.WaitingForEmployeeApproval,
                    "Finished" => Rent.RentStatus.Finished,
                    _ => throw new HttpRequestException("Unknown rent status returned from external provider")
                };
                rent.Status = status;
                if (rentStatusDto.EndDate != null)
                {
                    rent.EndDate = rentStatusDto.EndDate;
                }
            }
            else
            {
                throw new ArgumentException("Unknown provider id");
            }
        }
        await _context.SaveChangesAsync();
        
        return user.Rents.Select(RentDto.FromRent).ToList();
    }

    public async Task StartRentReturnAsync(Rent rent)
    {
        if (rent.ProviderId == _carRentalExternalProviderService.GetProviderId())
        {
           await _carRentalExternalProviderService.StartRentReturn(rent.ExternalRentId);
        }
        else
        {
            throw new ArgumentException("Unknown provider id");
        }
    }
}