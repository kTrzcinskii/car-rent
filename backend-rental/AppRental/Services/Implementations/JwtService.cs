using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AppRental.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace AppRental.Services.Implementations
{
    public class JwtService: IJwtService
    {
        private readonly IConfiguration _configuration;

        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateRentConfirmationToken()
        {
            var secret = _configuration["Jwt:Secret"];
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret!));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                expires: DateTime.UtcNow.AddMinutes(10),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerateLink(int rentId)
        {
            var token = GenerateRentConfirmationToken();
            return $"https://localhost:5001/api/offer/confirm?rentId={rentId}&token={token}"; // hardcoded origin
        }
    }
}