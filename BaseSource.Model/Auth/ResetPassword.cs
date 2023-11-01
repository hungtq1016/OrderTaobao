
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseSource.Model
{
    [Table("RESET_PASSWORD")]
    public class ResetPassword : BaseEntity
    {

        [Column("EMAIL")]
        [ForeignKey("User")]
        public string Email { get; set; } = string.Empty;
        [Column("IS_VERIFY")]
        public bool IsVerify { get; set; }

        public DateTime ExpiredTime { get; set; }

        public User? User { get; set; } = null!;

    }
}
