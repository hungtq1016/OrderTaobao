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

        [Column("CUSTOMER_ID", TypeName = "varchar"), MaxLength(36)]
        [ForeignKey("Customer")]
        public Guid CustomerId { get; set; }

        public Customer Customer { get; set; } = null!;

        [Column("ORDER_ID", TypeName = "varchar"), MaxLength(36)]
        [ForeignKey("Order")]
        public Guid OrderID { get; set; }

        public Order Order { get; set; } = null!;
    }
}
