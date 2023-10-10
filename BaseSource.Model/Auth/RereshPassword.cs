using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BaseSource.Model.Auth
{
    [Table("REFRESH_PASSWORD")]
    public class RefreshPassword : BaseEntity
    {
        [Column("EMAIL"),MaxLength(4)]
        public string Email { get; set; } = string.Empty;
        [Column("IS_VERIFY")]
        public bool IsVerify { get; set; } = false;

    }
}
