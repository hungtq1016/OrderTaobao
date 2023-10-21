﻿using Microsoft.AspNetCore.Identity;

namespace BaseSource.Model
{
    public class User : IdentityUser<string>
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        public bool Enable { get; set; }
        public ICollection<AuthHistory> AuthHistory { get; } = new List<AuthHistory>();
        public ICollection<UserHistory> UserHistory { get; } = new List<UserHistory>();
        public ICollection<ResetPassword> ResetPassword { get; } = new List<ResetPassword>();
        public ICollection<Notification> Notifications { get; } = new List<Notification>();
        public ICollection<Order> Orders { get; } = new List<Order>();
        //public virtual ICollection<UserRole> UserRole { get; set; }
        public List<AspNetUserRoles> Roles { get; } = new();

        public User()
        {
            Enable = true;
        }

    }
}
