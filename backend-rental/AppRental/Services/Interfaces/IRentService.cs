﻿using AppRental.DTO;
using AppRental.Model;

namespace AppRental.Services.Interfaces;

public interface IRentService
{
    Task<Rent?> GetByIdAsync(int id);
    Task<Rent> CreateRent(Offer offer, RentDTO rentDto);
    Task ConfirmRent(Rent rent);
}