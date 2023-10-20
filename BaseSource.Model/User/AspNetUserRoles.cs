
using Microsoft.AspNetCore.Identity;

namespace BaseSource.Model
{

    public class AspNetUserRoles : IdentityUserRole<string>
    {
        public  User User { get; set; }
        public  Role Role { get; set; }
    }
}
