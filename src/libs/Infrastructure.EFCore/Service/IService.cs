namespace Infrastructure.EFCore.Service
{
    public interface IService<TEntity, TRequest, TResponse>
    {
        Task<Response<PaginationResponse<List<TResponse>>>> FindPageAsync(PaginationRequest request, string route);
        Task<Response<List<TResponse>>> FindAllAsync(params string[] properties);
        Task<Response<TResponse>> FindByIdAsync(Guid id);
        Task<Response<TResponse>> FindByParamsAsync(Guid id, params string[] properties);
        Task<Response<TResponse>> FindOneAsync(Expression<Func<TEntity, bool>>[] conditions);
        Task<Response<TResponse>> AddAsync(TRequest request);
        Task<Response<TResponse>> EditAsync(Guid id, TRequest request);
        Task<Response<List<TResponse>>> BulkEditAsync(List<TRequest> request);
        Task<Response<bool>> DeleteAsync(Guid id);
        Task<Response<bool>> BulkDeleteAsync(List<TRequest> request);
    }
}
