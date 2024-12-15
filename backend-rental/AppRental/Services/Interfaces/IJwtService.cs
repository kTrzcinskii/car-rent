using Microsoft.AspNetCore.Identity;

namespace AppRental.Services.Interfaces
{
    public interface IJwtService
    {
        string GenerateRentConfirmationToken(int rentId);
        string GenerateLink(int rentId);
        string GenerateWorkerToken(IdentityUser user);
    }
}