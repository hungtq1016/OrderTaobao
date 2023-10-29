using BaseScource.Data;
using BaseSource.Dto;
using BaseSource.Helper;
using BaseSource.Model;
using Microsoft.EntityFrameworkCore;

namespace BaseSource.BackendAPI.Services
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<PageResponse<List<T>>> GetPagedDataAsync(PaginationRequest request, string route, IUriService uriService,bool enable);

        Task<List<T>> ReadAllAsync();

        Task<T> ReadByIdAsync(string id);

        Task AddAsync(T entity, string user);

        Task DeleteAsync(T entity, string user);

        Task EraseAsync(T entity);

        Task UpdateAsync(T entity, string user);

        Task Save();
    }
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly DataContext _context;
        private DbSet<T> entities;

        public Repository(DataContext context)
        {
            _context = context;
            entities = _context.Set<T>();
        }

        public async Task<PageResponse<List<T>>> GetPagedDataAsync(PaginationRequest request,string route, IUriService uriService, bool enable)
        {
            var validFilter = new PaginationRequest(request.PageNumber, request.PageSize);

            UInt16 totalRecords = Convert.ToUInt16(await entities.CountAsync());

            var lists = await entities
                .Where(e => e.Enable == enable)
                .OrderByDescending(e => e.UpdatedAt)
               .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
               .Take(validFilter.PageSize).ToListAsync();

            var pagedReponse = PaginationHelper.CreatePagedReponse<T>(lists, validFilter, totalRecords, uriService, route);

            return pagedReponse;
        }

        public async Task<List<T>> ReadAllAsync()
        {
            List<T> records = await entities.ToListAsync();

            return records;
        }

        public async Task<T> ReadByIdAsync(string id)
        {
            var customer = await entities.FirstOrDefaultAsync(t => t.Id == id && t.Enable);

            if (customer == null)
                return null!;
            return customer;
        }

        public async Task AddAsync(T entity, string user = "admin")
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            Id(entity);
            Created(entity, user);
            Updated(entity, user);
            Enable(entity);

            await entities.AddAsync(entity);
            await Save();
        }

        public async Task DeleteAsync(T entity, string user = "admin")
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            Updated(entity, user);
            Disable(entity);

            await Save();
        }

        public async Task EraseAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);

            await Save();
        }

        public async Task UpdateAsync(T entity, string user)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            Updated(entity, user);

            await Save();
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        private void Id(T entity)
        {
            entity.Id = Guid.NewGuid().ToString();
        }

        private void Created(T entity, string user)
        {
            entity.CreatedBy = user;
            entity.CreatedAt = DateTime.Now;
        }

        private void Updated(T entity, string user)
        {
            entity.UpdatedBy = user;
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
