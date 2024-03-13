
namespace AudioService.Infrastructure
{
    public interface IAudioService : IFileService<Audio, AudioRequest, AudioResponse, AudioExtensionEnums>
    {
        FileResponse FindByPath(string path);
    }
    public class CAudioService : FileService<Audio, AudioRequest, AudioResponse, AudioExtensionEnums>
        , IAudioService
    {
        public CAudioService(IRepository<Audio> repository, IMapper mapper, IUriService uriService) : base(repository, mapper, uriService)
        {

        }

        public FileResponse FindByPath(string path)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), $"Upload/{typeof(Audio).Name}", path);
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
