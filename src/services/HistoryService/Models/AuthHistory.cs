using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseSource.Model
{
    [Table("AUTH_HISTORY")]
    public class AuthHistory : BaseEntity
    {

        [Column("CONTENT", TypeName = "nvarchar"), MaxLength(100)]
        public string Content { get; set; } = string.Empty;

        [Column("USER_ID", TypeName = "nvarchar"), MaxLength(450)]
        [ForeignKey("User")]
        public string? UserId { get; set; }
        public User? User { get; set; } = null!;

    }
}
