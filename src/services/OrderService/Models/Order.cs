using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseSource.Model
{
    [Table("ORDERS")]
    public class Order : BaseEntity
    {

        [Column("STATUS")]
        public Byte Status { get; set; }

        [Column("USER_ID", TypeName = "nvarchar"), MaxLength(450)]
        [ForeignKey("User")]
        [NotMapped]
        public string? UserId { get; set; }

        [NotMapped]
        public User? User { get; set; }

        [NotMapped]
        public Notification? Notification { get; set; }

        [NotMapped]
        public ICollection<OrderDetail>? Details { get; } = new List<OrderDetail>();
    }
}
