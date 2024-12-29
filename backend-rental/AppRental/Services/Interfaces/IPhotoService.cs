using AppRental.Model;

namespace AppRental.Services.Interfaces
{
    public interface IPhotoService
    {
        Task AddPhotosToAzureAsync(Rent rent, List<IFormFile> photos);
    }
}