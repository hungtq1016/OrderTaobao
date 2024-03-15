﻿using Core;

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
        private readonly ElasticsearchClient client;

        protected RepositoryBase(TDbContext context, IMemoryCache cache)
        {
            _context = context;
            _entity = context.Set<TEntity>();
            _cache = cache;
            indexName = typeof(TEntity).Name.ToLower();
            client = new ElasticsearchClient();
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

            await client.IndexAsync(entity, indexName);

            return entity;
        }

        public async ValueTask DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            _entity.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

            await client.DeleteAsync(indexName, entity.Id);
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

            await client.UpdateAsync<TEntity, TEntity>(indexName, entity.Id, u => u.Doc(entity));

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
                await client.UpdateAsync<TEntity, TEntity>(indexName, entity.Id, u => u.Doc(entity));
            }
        }
     
    }
}