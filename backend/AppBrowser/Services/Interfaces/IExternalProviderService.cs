using AppBrowser.DTOs;

namespace AppBrowser.Services.Interfaces;

public interface IExternalProviderService
{
    int GetProviderId();
    Task<List<CarDto>> SearchCars(string brandName, string modelName);
}