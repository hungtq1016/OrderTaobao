using AutoMapper;
using ImageService.Models;
using ImageService.DTOs;
using ImageService.Enums;
using Infrastructure.EFCore.DTOs;
using Infrastructure.EFCore.Helpers;
using Infrastructure.EFCore.Repository;
using Infrastructure.EFCore.Service;

namespace ImageService.Infrastructure
{
    public interface IImageService : IFileService<Image,ImageRequest,ImageResponse, ImageExtensionsEnum>
    {
        FileResponse FindByPath(string path);
    }
    public class CImageService : FileService<Image, ImageRequest, ImageResponse, ImageExtensionsEnum>, IImageService
    {
        public CImageService(IRepository<Image> repository, IMapper mapper, IUriService uriService) : base(repository, mapper, uriService)
        {
            
        }

        public FileResponse FindByPath(string path)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), $"Upload/{typeof(Image).Name}", path);
            string extension = FileHelper.GetExtension(path);
            if (File.Exists(filePath))
            {
                byte[] bytes = File.ReadAllBytes(filePath);
                return new FileResponse
                {
                    FilesBytes = bytes,
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
