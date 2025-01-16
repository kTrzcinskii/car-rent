using AppBrowser.Infrastructure;
using AppBrowser.Model;
using AppBrowser.Services.Implementations;
using AppBrowser.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using AppBrowser.DTOs;

namespace Tests
{
    public class UserServiceTests
    {
        private readonly UserService _userService;
        private readonly DataContext _context;
        public UserServiceTests()
        {
            // Dependencies
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new DataContext(options);

            // SUT
            _userService = new UserService(_context);
        }

        [Fact]
        public async Task UserService_GetUserByEmailAsync_ReturnsUser()
        {
            // Arrange
            string email = "a@b.c";
            _context.Users.Add(new User {
                DateOfBirth = DateTime.Now, DateOfLicenseObtained = DateTime.Now, Email = email,
                FirstName = "A", LastName = "B", Location = "C", Offers = [], Rents = [], UserId = 1 });
            _context.SaveChanges();

            // Act
            var result = await _userService.GetUserByEmailAsync(email);

            // Assert
            result.Should().BeOfType<User>();
            result.Email.Should().Be(email);
        }

        [Fact]
        public async Task UserService_CreateUserAsync_CreatesUser()
        {
            // Arrange
            string email = "a@b.c";
            CreateUserDto userDto = new CreateUserDto { FirstName = "Test", LastName = "B", Location = "C" };
            _context.SaveChanges();

            // Act
            var res1 = _context.Users.Where((User u) => u.FirstName == "Test").Count();
            var result = await _userService.CreateUserAsync(email, userDto);
            var res2 = _context.Users.Where((User u) => u.FirstName == "Test").Count();

            // Assert
            result.Should().BeOfType<User>();
            result.Email.Should().Be(email);
            (res2 - res1).Should().Be(1);
        }
    }
}
