using System.Security.Claims;
using BaseSource.Model;
using Microsoft.AspNetCore.Identity;

namespace BaseSource.Dto
{
    public class RoleResponse : IdentityRole
    {
        public IList<Claim> Claims { get; set; }
    }
}
