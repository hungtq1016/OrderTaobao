using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace BaseSource.Model
{
    public class UserLogin : IdentityUserLogin<string>
    {
        public DateTime CreatedAt { get; set;}
    }
}
