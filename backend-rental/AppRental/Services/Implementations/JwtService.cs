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

        public string GenerateRentConfirmationToken(int rentId)
        {
            var secret = _configuration["Jwt:Secret"];
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret!));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim("rentId", rentId.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(10),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerateLink(int rentId)
        {
            var token = GenerateRentConfirmationToken(rentId);
            // TODO: generate this url dynamically
            return $"https://localhost:5001/api/rent/confirm-rent?token={token}"; // hardcoded origin
        }
    }
}