
namespace BaseSource.BackendAPI.Services
{
    public interface IRoleRepository
    {
        void CreateRole(Guid customerId, string user);
        void Save();

    }
}
