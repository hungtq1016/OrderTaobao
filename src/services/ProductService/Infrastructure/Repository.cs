using Nest;

namespace ProductService.Repository
{
    public class ProductRepository<TEntity> : RepositoryBase<ProductContext, TEntity> where TEntity : Entity
    {
        private readonly ILogger<ProductRepository<TEntity>> _logger;

        public ProductRepository(ProductContext context, IMemoryCache cache, IElasticClient elasticClient, ILogger<ProductRepository<TEntity>> logger) : base(context, cache, elasticClient, logger)
        {
            _logger = logger;
        }
    }
}
