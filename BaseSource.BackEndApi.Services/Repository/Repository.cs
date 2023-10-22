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
