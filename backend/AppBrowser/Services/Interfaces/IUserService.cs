﻿using System.Security.Claims;
using AppBrowser.DTOs;
using AppBrowser.Model;

namespace AppBrowser.Services.Interfaces;

public interface IUserService
{
    Task<User?> GetUserByEmailAsync(string email);
    Task<User> CreateUserAsync(string email, CreateUserDto createUserDto);
    UserInfoDto GetUserInfoFromClaims(IEnumerable<Claim> claims);
}