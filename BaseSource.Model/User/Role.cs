
using Microsoft.AspNetCore.Identity;

namespace BaseSource.Model
{
    public class Role : IdentityRole
    {
        //public virtual ICollection<UserRole> UserRole { get; set; }

        public List<AspNetUserRoles> Users { get; } = new();
    }
}
