
using BaseSource.Model;

namespace BaseSource.BackendAPI.Services
{
    public interface IRepository<T> where T : BaseEntity
    {
        IEnumerable<T> GetAll();
        T GetById(Guid id);
        void Create(T entity, string user);
        void Delete(T entity, string user);
        void AbsoluteDelete(T entity);
        void Update(T entity, string user);
        void Save();
    }
}
