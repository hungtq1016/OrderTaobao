using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseSource.Model
{
    [Table("NOTIFICATIONS")]
    public class Notification : BaseEntity
    {

        [Column("CONTENT", TypeName = "nvarchar"), MaxLength(255)]
        public string Content { get; set; } = string.Empty;

        [Column("IS_READ")]
        public bool IsRead { get; set; } = false;

        [Column("USER_ID", TypeName = "nvarchar"), MaxLength(450)]
        [ForeignKey("User")]
        public string UserId { get; set; }
        public User User { get; set; } = null!;

        [Column("ORDER_ID", TypeName = "varchar"), MaxLength(36)]
        [ForeignKey("Order")]
        public string OrderID { get; set; }

        public Order Order { get; set; } = null!;
    }
}
