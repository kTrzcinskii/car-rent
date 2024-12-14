using AppBrowser.DTOs;
using AppBrowser.Model;

namespace AppBrowser.Services.Interfaces;

public interface IExternalProviderService
{
    int GetProviderId();
    Task<List<Car>> SearchCars(string brandName, string modelName);
    Task<List<Car>> SyncCarsInDB(List<CarFromProviderDto> cars);
}