using BaseScource.Data;
using BaseSource.Model;
using Microsoft.EntityFrameworkCore;

namespace BaseSource.BackendAPI.Services
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAll();
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

        public async Task<IEnumerable<T>> GetAll()
        {
            return await entities.ToListAsync();
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

            entity.CreatedBy = user;
            entity.UpdatedBy = user;

            await entities.AddAsync(entity);
            await Save();
        }

        public async Task Delete(T entity, string user = "admin")
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            entity.UpdatedAt = DateTime.Now;
            entity.UpdatedBy = user;
            entity.Enable = false;

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
            entity.UpdatedAt = DateTime.Now;
            entity.UpdatedBy = user;

            await Save();
        }
        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

    }
}
