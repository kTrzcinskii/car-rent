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
            return $"https://localhost:5001/api/confirm?rentId={rentId}&token={token}"; // hardcoded origin
        }

        public (ClaimsPrincipal?, string? error) ValidateRentConfirmationToken(string token)
        {
            var secret = _configuration["Jwt:Secret"];
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret!));
            var tokenHandler = new JwtSecurityTokenHandler();

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidIssuer = _configuration["Jwt:Issuer"],
                ValidAudience = _configuration["Jwt:Audience"],
                IssuerSigningKey = key,
                ClockSkew = TimeSpan.Zero
            };    

            SecurityToken? validatedToken = null;
            ClaimsPrincipal? principal = null;

            try
            {
                principal = tokenHandler.ValidateToken(token, validationParameters, out validatedToken);
            }
            catch (SecurityTokenExpiredException)
            {
                return (null, "expired");
            }
            catch (Exception)
            {
                return (null, "invalid");
            }

            var jwtToken = validatedToken as JwtSecurityToken;
            if (jwtToken == null) //|| !jwtToken.Claims.Any(x => x.Type == "rentId"))
                return (null, "invalid");

            return (principal, null);
        }
    }
}