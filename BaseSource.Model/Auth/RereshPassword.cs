using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseSource.Model.Auth
{
    [Table("REFRESH_PASSWORD")]
    public class RefreshPassword : BaseEntity
    {
        [Column("EMAIL"), MaxLength(4)]
        public string Email { get; set; } = string.Empty;
        [Column("IS_VERIFY")]
        public bool IsVerify { get; set; } = false;

    }
}
