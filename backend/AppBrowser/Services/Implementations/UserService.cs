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

    public Task<User> CreateUser(CreateUserDTO createUserDto)
    {
        throw new NotImplementedException();
    }
}