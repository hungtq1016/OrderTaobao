
using BaseSource.BackendAPI.Services.Helpers;
using BaseSource.Dto;
using BaseSource.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BaseSource.BackendAPI.Services
{
    public interface IImageService
    {
        Task<Response<bool>> CreateImage(List<IFormFile> files, string user);
        Task<Response<List<Image>>> ReadAllImages(string user);
        Task<Response<PageResponse<List<Image>>>> ReadPageImages([FromQuery] PaginationRequest request, string route, bool enable);
        ImageResponse ReadImage(string name);
        Task<Response<bool>> DeleteImage(string id, string user);
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

        public async Task<Response<bool>> CreateImage(List<IFormFile> files, string user)
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
                await _imageRepo.AddAsync(img, user);
            }

            return ResponseHelper.CreateCreatedResponse(true);
        }

        public async Task<Response<List<Image>>> ReadAllImages(string user)
        {
            List<Image> images = await _imageRepo.ReadAllAsync();

            return ResponseHelper.CreateSuccessResponse(images);
        }

        public async Task<Response<PageResponse<List<Image>>>> ReadPageImages([FromQuery] PaginationRequest request, string route, bool enable)
        {
            PageResponse<List<Image>> images = await _imageRepo.GetPagedDataAsync(request,route, _uriService,enable);

            return ResponseHelper.CreateSuccessResponse<PageResponse<List<Image>>>(images);
        }

        public async Task<Response<bool>> DeleteImage(string id,string user)
        {
            Image image = await _imageRepo.ReadByIdAsync(id);

            if(image is null)
                return ResponseHelper.CreateErrorResponse<bool>
                       (404, "No image found.");

            await _imageRepo.DeleteAsync(image,user);

            string filepath = Path.Combine(Directory.GetCurrentDirectory(), $"Upload\\{image.Type}\\{image.Url}");

            if (File.Exists(filepath))
                File.Delete(filepath);

            return ResponseHelper.CreateSuccessResponse(true);
        }

        public ImageResponse ReadImage(string name)
        {
            string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "Upload/Image", name);
            string extension = FileHelper.GetExtension(name);
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
