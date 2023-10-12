using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace BaseSource.Model
{
    public class UserRoles
    {
        public const string Admin = "Admin";
        public const string Manager = "Manager";
        public const string Staff = "Staff";
        public const string Customer = "Customer";
        public const string Collaborator = "Collaborator";
        public const string SuperAdmin = "SuperAdmin";
        public const string Visitor = "Visitor";
    }
}
