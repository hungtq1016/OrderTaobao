using BaseScource.Data;
using BaseSource.Dto;
using BaseSource.Helper;
using BaseSource.Model;
using Microsoft.EntityFrameworkCore;

namespace BaseSource.BackendAPI.Services
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<PageResponse<List<T>>> GetAll(PaginationRequest request, string route, IUriService uriService);

        Task<T> GetById(string id);

        Task Create(T entity, string user);

        Task Delete(T entity, string user);

        Task AbsoluteDelete(T entity);

        Task Update(T entity, string user);

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

        public async Task<PageResponse<List<T>>> GetAll(PaginationRequest request,string route, IUriService uriService)
        {
            var validFilter = new PaginationRequest(request.PageNumber, request.PageSize);

            var totalRecords = await entities.CountAsync();

            var lists = await entities
               .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
               .Take(validFilter.PageSize).ToListAsync();

            var pagedReponse = PaginationHelper.CreatePagedReponse<T>(lists, validFilter, totalRecords, uriService, route);

            return pagedReponse;
        }

        public async Task<T> GetById(string id)
        {
            var customer = await entities.FirstOrDefaultAsync(t => t.Id == id && t.Enable);

            if (customer == null)
                return null!;
            return customer;
        }

        public async Task Create(T entity, string user = "admin")
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

        public async Task Delete(T entity, string user = "admin")
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            Updated(entity, user);
            Disable(entity);

            await Save();
        }

        public async Task AbsoluteDelete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);

            await Save();
        }

        public async Task Update(T entity, string user)
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
