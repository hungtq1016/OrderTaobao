
using BaseSource.BackendAPI.Services.Helpers;
using BaseSource.Dto;
using BaseSource.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BaseSource.BackendAPI.Services
{
    public interface IImageService
    {
        Task<Response<List<Image>>> Get();

        Task<Response<PageResponse<List<Image>>>> GetPagedData([FromQuery] PaginationRequest request, string route, bool enable);

        ImageResponse GetByPath(string path);

        Task<Response<bool>> Add(List<IFormFile> files);
        
        Task<Response<bool>> Erase(string id);
    }

    public class ImageService : IImageService
    {
        private readonly IRepository<Image> _imageRepo;
        private readonly IUriService _uriService;

        public ImageService(IRepository<Image> imageRepo, IUriService uriService)
        {
            _imageRepo = imageRepo;
            _uriService = uriService;

        }

        public async Task<Response<bool>> Add(List<IFormFile> files)
        {
            List<FileResponse> images = await FileHelper.WriteFile(files,"Image", "Image");

            foreach (FileResponse image in images)
            {

                Image img = new Image()
                {
                    Size = image.Size,
                    Type = image.Type,
                    Label = image.Name,
                    Url = image.Path,

                };
                await _imageRepo.AddAsync(img);
            }

            return ResponseHelper.CreateCreatedResponse(true);
        }

        public async Task<Response<List<Image>>> Get()
        {
            List<Image> images = await _imageRepo.ReadAllAsync();

            return ResponseHelper.CreateSuccessResponse(images);
        }

        public async Task<Response<PageResponse<List<Image>>>> GetPagedData([FromQuery] PaginationRequest request, string route, bool enable)
        {
            PageResponse<List<Image>> images = await _imageRepo.GetPagedDataAsync(request,route, _uriService,enable);

            return ResponseHelper.CreateSuccessResponse(images);
        }

        public async Task<Response<bool>> Erase(string id)
        {
            Image image = await _imageRepo.ReadByIdAsync(id);

            if(image is null)
                return ResponseHelper.CreateErrorResponse<bool>
                       (404, "No image found.");

            await _imageRepo.DeleteAsync(image);

            string filepath = Path.Combine(Directory.GetCurrentDirectory(), $"Upload/{image.Type}/{image.Url}");

            if (File.Exists(filepath))
                File.Delete(filepath);

            return ResponseHelper.CreateSuccessResponse(true);
        }

        public ImageResponse GetByPath(string path)
        {
            string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "Upload/Image", path);
            string extension = FileHelper.GetExtension(path);
            if (File.Exists(imagePath))
            {        
                byte[] imageBytes = File.ReadAllBytes(imagePath);
                return new ImageResponse
                {
                    ImageBytes = imageBytes,
                    Extension = extension == "svg" ? "svg+xml" : extension
                };
            }
            else
            {
                return null!;
            }
        }
    }
}
