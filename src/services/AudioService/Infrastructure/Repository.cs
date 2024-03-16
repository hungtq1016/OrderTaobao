using Nest;

namespace AudioService.Infrastructure
{
    public class AudioRepository<TEntity> : RepositoryBase<AudioContext, TEntity> where TEntity : Entity
    {
        private readonly ILogger<AudioRepository<TEntity>> _logger;

        public AudioRepository(AudioContext context, IMemoryCache cache, IElasticClient elasticClient, ILogger<AudioRepository<TEntity>> logger) : base(context, cache, elasticClient, logger)
        {
            _logger = logger;
        }
    }
}
