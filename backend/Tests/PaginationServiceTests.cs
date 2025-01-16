using AppBrowser.Infrastructure;
using AppBrowser.Model;
using AppBrowser.Services.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FluentAssertions;
using AppBrowser.DTOs;

namespace Tests
{
    public class PaginationServiceTests
    {

        private readonly PaginationService _service;
        public PaginationServiceTests() {
            _service = new PaginationService();
        }

        [Fact]
        public async Task PaginationService_Works()
        {
            // Arrange
            int[] s1 = { 1, 2, 3, 4, 5, 6};
            int[] s2 = { };

            // Act
            var result1 = await Task.Run(() => _service.GetPaginatedResponse(s1, 0, 4));
            var result2 = await Task.Run(() => _service.GetPaginatedResponse(s1, 1, 4));
            var result3 = await Task.Run(() => _service.GetPaginatedResponse(s2, 1, 4));

            // Assert
            result1.Should().NotBeNull();
            result1.Should().BeOfType<PaginatedDto<int>>();
            result1.Data.Should().HaveCount(4);

            result2.Should().NotBeNull();
            result2.Should().BeOfType<PaginatedDto<int>>();
            result2.Data.Should().HaveCount(2);

            result3.Should().NotBeNull();
            result3.Should().BeOfType<PaginatedDto<int>>();
            result3.Data.Should().HaveCount(0);
        }
    }
}
