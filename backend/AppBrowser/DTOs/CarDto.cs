namespace AppBrowser.DTOs;

public class CarDto
{
    public string UUID { get; set; }
    public string BrandName { get; set; }
    public string ModelName { get; set; }
    public int ProductionYear { get; set; }
    public string? ImageUrl { get; set; }
    public string Localization { get; set; }
}