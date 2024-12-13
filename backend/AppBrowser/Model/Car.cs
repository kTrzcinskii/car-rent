namespace AppBrowser.Model;

public class Car
{
    public int CarId { get; set; }
    public int ProviderId { get; set; }
    public int ExternalCarId { get; set; }
    public string ModelName { get; set; }
    public string BrandName { get; set; }
    public string Location { get; set; }
    public int ProductionYear { get; set; }
    public string? ImageUrl { get; set; } = null;
}