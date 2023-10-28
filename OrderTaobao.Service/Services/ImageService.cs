
using BaseSource.BackendAPI.Services.Helpers;
using BaseSource.Dto;
using BaseSource.Model;
using Microsoft.AspNetCore.Http;

namespace BaseSource.BackendAPI.Services
{
    public interface IImageService
    {
        Task<Response<bool>> CreateImage(List<IFormFile> files);
        Task<List<Image>> ReadAllImages();
    }

    public class ImageService : IImageService
    {
        private readonly IRepository<Image> _imageRepo;

        public ImageService(IRepository<Image> imageRepo)
        {
            _imageRepo = imageRepo;
        }

        public async Task<Response<bool>> CreateImage(List<IFormFile> files)
        {
            List<FileResponse> images = await FileHelper.WriteFile(files,"Image", "Image");

            IEnumerable<Task> tasks = images.Select(async image =>
            {
                Image img = new Image()
                {
                    Size = image.Size,
                    Type = image.Type,
                    Label = image.Name,
                    Url = image.Path,
                    
                };
                await _imageRepo.AddAsync(img, "admin");
            });

            await Task.WhenAll(tasks);

            return ResponseHelper.CreateCreatedResponse(true);
        }

        public async Task<List<Image>> ReadAllImages()
        {
            List<Image> images = await _imageRepo.ReadAllAsync();

            return images;
        }


    }
}
