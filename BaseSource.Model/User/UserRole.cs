using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace BaseSource.Model
{
    public class UserRole : IdentityUserRole<string>
    {
        public  User User { get; set; }
        public  Role Role { get; set; }
    }
}
