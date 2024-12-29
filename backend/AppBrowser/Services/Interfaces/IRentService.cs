using AppBrowser.DTOs;
using AppBrowser.Model;

namespace AppBrowser.Services.Interfaces;

public interface IRentService
{
    Task<Rent?> GetByIdAsync(int id);
    Task<List<RentDto>> FindUserRents(User user);
    Task StartRentReturnAsync(Rent rent);
}