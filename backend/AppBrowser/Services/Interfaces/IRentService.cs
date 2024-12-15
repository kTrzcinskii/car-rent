using AppBrowser.DTOs;
using AppBrowser.Model;

namespace AppBrowser.Services.Interfaces;

public interface IRentService
{
    Task<List<RentDto>> FindUserRents(User user);
}