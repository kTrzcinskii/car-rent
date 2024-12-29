using AppRental.Infrastructure;
using AppRental.Model;
using AppRental.Services.Interfaces;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace AppRental.Services.Implementations
{
    public class PhotoService : IPhotoService
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly DataContext _context;
        private readonly IConfiguration _config;

        public PhotoService(DataContext context, BlobServiceClient blobServiceClient,
            IConfiguration config)
        {
            _config = config;
            _context = context;
            _blobServiceClient = blobServiceClient;
        }

        public async Task AddPhotosToAzureAsync(Rent rent, List<IFormFile> photos)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(_config["AzureBlob:ReturnContainer"]);
            await containerClient.CreateIfNotExistsAsync(PublicAccessType.Blob);

            rent.PhotoUrls ??= new List<string>();

            var uploadTasks = photos.Select(async photo =>
            {
                string blobName = Guid.NewGuid().ToString() + photo.FileName;
                await containerClient.UploadBlobAsync(blobName, photo.OpenReadStream());

                rent.PhotoUrls.Add(containerClient.Uri.ToString());
            });
            await Task.WhenAll(uploadTasks);
            await _context.SaveChangesAsync();
        }


    }
}