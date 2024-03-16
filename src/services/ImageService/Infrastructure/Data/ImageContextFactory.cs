using Infrastructure.EFCore.Data;

namespace ImageService.Data
{
    public class ImageContextFactory : AppDbContextFactory<ImageContext>
    {
        public ImageContextFactory() : base("imageDB.docker")
        {
        }
    }
}
