using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AppBrowser.Model;
using AppBrowser.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace AppBrowser.Services.Implementations;

public class JwtService : IJwtService
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<JwtService> _logger;

    public JwtService(IConfiguration configuration, ILogger<JwtService> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    public string GenerateToken(User user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.Name, user.FirstName),
            new("Role", "User"),
        };

        return GenerateTokenFromClaims(claims);
    }

    public string GenerateRegistrationToken(string email)
    {
        var claims = new List<Claim>
        {
            new (ClaimTypes.Email, email),
            new ("CanFinishRegistration", "true"),
        };

        return GenerateTokenFromClaims(claims, 20);
    }

    private string GenerateTokenFromClaims(IEnumerable<Claim> claims, int minutes = 60 * 24)
    {
        var secret = _configuration["Jwt:Secret"];
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(minutes),
            signingCredentials: creds
            );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}