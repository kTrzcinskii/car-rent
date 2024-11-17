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
}