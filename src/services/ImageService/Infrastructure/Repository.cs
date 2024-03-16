using ImageService.Data;
using ImageService.Models;
using Infrastructure.EFCore.Repository;
using Microsoft.Extensions.Caching.Memory;
using Nest;

namespace ImageService.Infrastructure
{
    public class ImageRepository : RepositoryBase<ImageContext, Image>  
    {
        private readonly ILogger<ImageRepository> _logger;
        public ImageRepository(ImageContext context, IMemoryCache cache, IElasticClient elasticClient, ILogger<ImageRepository> logger) : base(context, cache, elasticClient, logger)
        {
            _logger = logger;
        }
    }
}
