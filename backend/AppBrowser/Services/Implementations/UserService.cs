using System.Security.Claims;
using AppBrowser.DTOs;
using AppBrowser.Infrastructure;
using AppBrowser.Model;
using AppBrowser.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AppBrowser.Services.Implementations;

public class UserService : IUserService
{
    private readonly DataContext _context;

    public UserService(DataContext context)
    {
        _context = context;
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        return user;
    }

    public async Task<User> CreateUserAsync(string email, CreateUserDto createUserDto)
    {
        var user = new User
        {
            FirstName = createUserDto.FirstName,
            LastName = createUserDto.LastName,
            Email = email,
            Location = createUserDto.Location,
            DateOfBirth = createUserDto.DateOfBirth,
            DateOfLicenseObtained = createUserDto.DateOfLicenseObtained
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return user;
    }

    public UserInfoDto GetUserInfoFromClaims(IEnumerable<Claim> claims)
    {
        var email = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
        if (email == null)
        {
            throw new ArgumentException("Missing email in claims");
        }
    
        var firstName = claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
        if (firstName == null)
        {
            throw new ArgumentException("Missing firstName in claims");
        }

        var userInfo = new UserInfoDto
        {
            Email = email,
            FirstName = firstName
        };
        return userInfo;
    }
}