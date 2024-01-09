using BaseScource.Data;
using Basesource.Constants;
using BaseSource.Dto;
using BaseSource.Helper;
using BaseSource.Model;
using Microsoft.EntityFrameworkCore;

namespace BaseSource.BackendAPI.Services
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<PageResponse<List<T>>> GetPagedDataAsync(PaginationRequest request, string route, IUriService uriService);

        Task<List<T>> ReadAllAsync();

        Task<T> ReadByIdAsync(string id);

        Task AddAsync(T entity);

        Task DeleteAsync(T entity);

        Task EraseAsync(T entity);

        Task UpdateAsync(T entity);

        Task Save();
    }
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly DataContext _context;
        private DbSet<T> _entities;

        public Repository(DataContext context)
        {
            _context = context;
            _entities = _context.Set<T>();
        }

        public async Task<PageResponse<List<T>>> GetPagedDataAsync(PaginationRequest request,string route, IUriService uriService)
        {
            var validFilter = new PaginationRequest(request.PageNumber, request.PageSize);

            var totalRecords = Convert.ToUInt16(await _entities.CountAsync());

            var query = _entities.AsQueryable();

            if (request.Status == StatusEnum.Disable || request.Status == StatusEnum.Enable)
            {
                query = query.Where(e => e.Enable == (request.Status == StatusEnum.Enable));
            }

            var lists = await query
                .OrderByDescending(e => e.UpdatedAt)
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .ToListAsync();

            var pagedReponse = PaginationHelper.CreatePagedReponse(lists, validFilter, totalRecords, uriService, route);

            return pagedReponse;
        }

        public async Task<List<T>> ReadAllAsync()
        {
            List<T> records = await _entities.ToListAsync();

            return records;
        }

        public async Task<T> ReadByIdAsync(string id)
        {
            T? record = await _entities.FirstOrDefaultAsync(t => t.Id == id);
            return record!;
        }

        public async Task AddAsync(T entity)
        {
            Id(entity);
            Created(entity);
            Updated(entity);
            Enable(entity);

            await _entities.AddAsync(entity);
            await Save();
        }

        public async Task DeleteAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            Updated(entity);
            Disable(entity);

            await Save();
        }

        public async Task EraseAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _entities.Remove(entity);

            await Save();
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            Updated(entity);

            await _context.SaveChangesAsync();
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        private void Id(T entity)
        {
            entity.Id = Guid.NewGuid().ToString();
        }

        private void Created(T entity)
        {
            entity.CreatedAt = DateTime.Now;
        }

        private void Updated(T entity)
        {
            entity.UpdatedAt = DateTime.Now;
        }

        private void Enable(T entity)
        {
            entity.Enable = true;
        }

        private void Disable(T entity)
        {
            entity.Enable = false;
        }
    }
}
