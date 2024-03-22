namespace Infrastructure.EFCore.Repository
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        Task<TEntity> FindByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<TEntity> FindByParamsAsync(Guid id, params string[] properties);
        Task<TEntity> FindOneAsync(Expression<Func<TEntity, bool>>[] conditions, CancellationToken cancellationToken = default);
        Task<List<TEntity>> FindAllByConditionAsync(Expression<Func<TEntity, bool>>[] conditions, params string[] properties);
        Task<PaginationResponse<List<TEntity>>> FindPageAsync(PaginationRequest request, string route, IUriService uriService);
        Task<List<TEntity>> FindAllAsync(params string[] properties);
        Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task<TEntity> EditAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task<List<TEntity>> BulkEditAsync(List<TEntity> entities, CancellationToken cancellationToken = default);
        ValueTask DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
        ValueTask BulkDeleteAsync(List<TEntity> entities, CancellationToken cancellationToken = default);
    }
}
