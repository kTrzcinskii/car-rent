﻿using AppRental.DTO;
using AppRental.Infrastructure;
using AppRental.Model;
using AppRental.Services.Interfaces;

namespace AppRental.Services.Implementations;

public class RentService : IRentService
{
    private readonly DataContext _context;

    public RentService(DataContext context)
    {
        _context = context;
    }

    public async Task<Rent?> GetByIdAsync(int id)
    {
        var rent = await _context.Rents.FindAsync(id);
        return rent;
    }

    public async Task<Rent> CreateRent(Offer offer, RentDTO rentDto)
    {
        var rent = new Rent
        {
            Offer = offer,
            FirstName = rentDto.FirstName,
            LastName = rentDto.LastName,
            Email = rentDto.Email,
        };
        _context.Rents.Add(rent);
        await _context.SaveChangesAsync();
        return rent;
    }

    public async Task ConfirmRent(Rent rent)
    {
        rent.Confirmed = true;
        rent.StartDate = DateTime.UtcNow;
        rent.Offer.Car.Status = CarStatus.Rented;
        await _context.SaveChangesAsync();
    }
}