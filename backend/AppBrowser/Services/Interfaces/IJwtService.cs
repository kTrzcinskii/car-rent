using AppBrowser.Model;

namespace AppBrowser.Services.Interfaces;

public interface IJwtService
{
    string GenerateToken(User user);
    string GenerateRegistrationToken(string email);
}