using System.Security.Claims;

namespace AppRental.Services.Interfaces
{
    public interface IJwtService
    {
        string GenerateRentConfirmationToken();
        string GenerateLink(int rentId);
    }
}