
using BaseScource.Data;
using BaseSource.Model;

namespace BaseSource.BackendAPI.Services
{
    public class RoleRepository : IRoleRepository
    {
        private readonly DataContext _context;
        public RoleRepository(DataContext context)
        {
            _context = context;
        }

        public void CreateRole(Guid customerId, string user = "admin")
        {
            UserRole userRole = new UserRole();
            var role = _context.Roles.FirstOrDefault(r => r.Name == "Customer");
            userRole.Id = Guid.NewGuid();
            userRole.CustomerId = customerId;
            userRole.RoleId = role.Id;
            userRole.CreatedAt = DateTime.UtcNow;
            userRole.UpdatedAt = DateTime.UtcNow;
            userRole.CreatedBy = user;
            userRole.UpdatedBy = user;

            _context.UserRole.Add(userRole);
            Save();
        }
        public void Save()
        {
            _context.SaveChanges();
        }

    }
}
