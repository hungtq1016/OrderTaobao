using Core;
using Infrastructure.EFCore.DTOs;
using System.Linq.Expressions;

namespace Infrastructure.EFCore.Service
{
    public interface IService<TEntity,TRequest,TResponse>
    {
        Task<Response<List<TResponse>>> FindAllAsync();
        Task<Response<List<TResponse>>> FindAllAsync(params string[] properties);
        Task<Response<TResponse>> FindByIdAsync(Guid id);
        Task<Response<TResponse>> FindOneAsync(Expression<Func<TEntity, bool>>[] conditions);
        Task<Response<TResponse>> AddAsync(TRequest request);
        Task<Response<TResponse>> EditAsync(Guid id, TRequest request);
        Task<Response<TResponse>> BulkEditAsync(List<TEntity> request);
        Task<Response<bool>> DeleteAsync(Guid id);
        Task<Response<bool>> BulkDeleteAsync(List<TEntity> request);
    }
}
