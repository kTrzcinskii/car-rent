﻿namespace AppBrowser.DTOs;

public class CarRentalExternalProviderCarDto
{
    public int Id { get; set; }
    public string Brand { get; set; }
    public string Model { get; set; }
    public int ProductionYear { get; set; }
    public string Localization { get; set; }
    public string? ImageUrl { get; set; }
}