﻿/*using BaseScource.Data;
using BaseSource.Model;
using Microsoft.EntityFrameworkCore;

namespace BaseSource.BackendAPI.Services
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly DataContext _context;
        private DbSet<T> entities;
        public Repository(DataContext context)
        {
            _context = context;
            entities = _context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return  entities.ToList();
        }
        public T GetById(Guid id)
        {
            var customer = entities.FirstOrDefault(t =>  t.Id == id && t.Enable);

            if (customer == null)
                return null!;
            return customer;
        }

        public void Create(T entity, string user = "admin")
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entity.Id = Guid.NewGuid();
            entity.CreatedAt = DateTime.Now;
            entity.UpdatedAt = DateTime.Now;
            entity.CreatedBy = user;
            entity.UpdatedBy = user;

            entities.Add(entity);
            Save();
        }

        public void Delete(T entity, string user = "admin")
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            entity.UpdatedAt = DateTime.Now;
            entity.UpdatedBy = user;
            entity.Enable = false;

            Save();
        }

        public void AbsoluteDelete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);

            Save();
        }

        public void Update(T entity, string user)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entity.UpdatedAt = DateTime.Now;
            entity.UpdatedBy = user;

            Save();
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
*/