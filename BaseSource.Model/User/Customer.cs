using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BaseSource.Model;

namespace BaseSource.Model
{
    [Table("CUSTOMERS")]
    public class Customer : BaseUser
    {
        //public ICollection<UserRole> UserRoles { get; } = new List<UserRole>();

        [Column("REMEMBER_TOKEN", TypeName = "varchar"), MaxLength(36)]
        public Guid RememberToken { get; set; } 
        [Column("ENABLE")]
        public bool Enable { get; set; } = true;
        public Notification? Notification { get; set; }
        public EmailVerify? EmailVerify { get; set; }
        public AccessToken? AccessToken { get; set; }
        public ICollection<CustomerHistory> CustomerHistories { get; } = new List<CustomerHistory>();
        public ICollection<AuthHistory> AuthHistories { get; } = new List<AuthHistory>();
    }
}
