using System.Security.Claims;

namespace AppRental.Services.Interfaces
{
    public interface IJwtService
    {
        string GenerateRentConfirmationToken(int rentId);
        string GenerateLink(int rentId);
    }
}