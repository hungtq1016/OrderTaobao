namespace Infrastructure.EFCore.Repository
{
    public abstract class RepositoryBase<TDbContext, TEntity> : IRepository<TEntity>
        where TEntity : Entity
        where TDbContext : DbContext
    {
        protected readonly TDbContext _context;
        protected readonly DbSet<TEntity> _entity;
        private readonly IMemoryCache _cache;
        private readonly string indexName;
        private readonly IElasticClient _elasticClient;
        private readonly Microsoft.Extensions.Logging.ILogger _logger;

        protected RepositoryBase(TDbContext context, IMemoryCache cache, IElasticClient elasticClient, Microsoft.Extensions.Logging.ILogger logger)
        {
            _context = context;
            _entity = context.Set<TEntity>();
            _cache = cache;
            _elasticClient = elasticClient; 
            _logger = logger;
            indexName = typeof(TEntity).Name.ToLower();
        }

        public async Task<TEntity> FindByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var cacheKey = id;
            if (!_cache.TryGetValue(cacheKey, out TEntity entity))
            {
                entity = await _entity.FindAsync(id);

                if (entity != null)
                {
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromMinutes(5));

                    _cache.Set(cacheKey, entity, cacheEntryOptions);
                }
            }
            return entity;
        }

        public async Task<TEntity> FindByParamsAsync(Guid id, params string[] properties)
        {
            IQueryable<TEntity> query = _entity;

            foreach (var include in properties)
            {
                query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync(entity => entity.Id == id);
        }

        public async Task<TEntity> FindOneAsync(Expression<Func<TEntity, bool>>[] conditions, CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> query = _entity;
            foreach (var condition in conditions)
            {
                query = query.Where(condition);
            }

            return await query.FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<List<TEntity>> FindAllByConditionAsync(Expression<Func<TEntity, bool>>[] conditions, params string[] properties)
        {
            IQueryable<TEntity> query = _entity;
            if (conditions != null)
            {
                foreach (var condition in conditions)
                {
                    query = query.Where(condition);
                }
            }

            foreach (var include in properties)
            {
                query = query.Include(include);
            }

            return await query.ToListAsync();
        }

        public async Task<PaginationResponse<List<TEntity>>> FindPageAsync(PaginationRequest request, string route, IUriService uriService)
        {
            var validFilter = new PaginationRequest(request.PageNumber, request.PageSize, request.Status);

            IQueryable<TEntity> query = _entity;

            if (request.Status != EnableEnum.All)
            {
                query = query.Where(e => e.Enable == (request.Status == EnableEnum.Enabled));
            }

            int totalRecords = await query.CountAsync();

            var lists = await query
                .OrderByDescending(e => e.UpdatedAt)
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .ToListAsync();

            return PaginationHelper<TEntity>.GeneratePaginationResponse(lists, validFilter, totalRecords);
        }

        public async Task<List<TEntity>> FindAllAsync(params string[] properties)
        {
            IQueryable<TEntity> query = _entity;

            foreach (var include in properties)
            {
                query = query.Include(include);
            }

            return await query.ToListAsync();
        }

        public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await _entity.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            try
            {
                var response = await _elasticClient.IndexAsync(entity, idx => idx.Index(indexName));


                if (!response.IsValid)
                {
                    _logger.LogError("Failed to index entity in Elasticsearch. Error: {0}", response.OriginalException?.Message);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred while indexing entity in Elasticsearch.");
            }

            return entity;
        }

        public async ValueTask DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            _entity.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

            try
            {
                var response = await _elasticClient.DeleteAsync<TEntity>(entity.Id, idx => idx.Index(indexName));

                if (!response.IsValid)
                {
                    _logger.LogError($"Failed to delete {typeof(TEntity).Name} in Elasticsearch. Error: {0}", response.OriginalException?.Message);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred while indexing entity in Elasticsearch.");
            }
        }

        public async Task<TEntity> EditAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            if (!_context.Set<TEntity>().Local.Any(e => e.Id == entity.Id))
            {
                _context.Set<TEntity>().Attach(entity);
            }

            entity.UpdatedAt = DateTime.UtcNow;

            _context.Entry(entity).State = EntityState.Modified;

            await _context.SaveChangesAsync(cancellationToken);

            try
            {
                var response = await _elasticClient.UpdateAsync<TEntity, object>(entity.Id, u => u.Doc(entity).Index(indexName));

                if (!response.IsValid)
                {
                    _logger.LogError($"Failed to update {typeof(TEntity).Name} in Elasticsearch. Error: {0}", response.OriginalException?.Message);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred while indexing entity in Elasticsearch.");
            }

            return entity;
        }


        public async Task<List<TEntity>> BulkEditAsync(List<TEntity> entities, CancellationToken cancellationToken = default)
        {
            foreach (var entity in entities)
            {
                await EditAsync(entity, cancellationToken);
            }

            return entities;
        }

        public async ValueTask BulkDeleteAsync(List<TEntity> entities, CancellationToken cancellationToken = default)
        {
            _entity.RemoveRange(entities);
            await _context.SaveChangesAsync(cancellationToken);

            foreach (var entity in entities)
            {
                try
                {
                    var response = await _elasticClient.DeleteAsync<TEntity>(entity.Id, idx => idx.Index(indexName));

                    if (!response.IsValid)
                    {
                        _logger.LogError($"Failed to delete {typeof(TEntity).Name} in Elasticsearch. Error: {0}", response.OriginalException?.Message);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Exception occurred while indexing entity in Elasticsearch.");
                }
            }
        }
     
    }
}