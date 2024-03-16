using Nest;

namespace OAuth2Service.Repository
{
    public class OAuth2Repository<TEntity> : RepositoryBase<OAuth2Context, TEntity> where TEntity : Entity
    {
        private readonly ILogger<OAuth2Repository<TEntity>> _logger;

        public OAuth2Repository(OAuth2Context context, IMemoryCache cache, IElasticClient elasticClient, ILogger<OAuth2Repository<TEntity>> logger) : base(context, cache, elasticClient, logger)
        {
            _logger = logger;
        }
    }
}
