using Microsoft.AspNetCore.Identity;

namespace BaseSource.Model
{
    public class User : IdentityUser<string>
    {
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string RefreshToken { get; set; } = string.Empty;

        public DateTime RefreshTokenExpiryTime { get; set; }

        public bool Enable { get; set; }

        public Address? Address { get; set; }

        public ICollection<AuthHistory>? AuthHistory { get; } = new List<AuthHistory>();

        public ICollection<AuditTrailHistory>? AuditTrail { get; } = new List<AuditTrailHistory>();

        public ICollection<UserHistory>? UserHistory { get; } = new List<UserHistory>();

        public ICollection<ResetPassword>? ResetPassword { get; } = new List<ResetPassword>();

        public ICollection<Notification>? Notifications { get; } = new List<Notification>();

        public ICollection<Order>? Orders { get; } = new List<Order>();

        public List<AspNetUserRoles>? Roles { get; } = new();

        public List<ImageUser>? Images { get; } = new();

    }
}
