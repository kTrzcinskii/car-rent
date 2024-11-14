using AppBrowser.DTOs;
using AppBrowser.Model;

namespace AppBrowser.Services.Interfaces;

public interface IUserService
{
    Task<User?> GetUserByEmailAsync(string email);
    Task<User> CreateUser(CreateUserDTO createUserDto);
}