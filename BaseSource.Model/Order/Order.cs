using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BaseSource.Model
{
    [Table("ORDERS")]
    public class Order : BaseEntity
    {

        [Column("STATUS")]
        public int Status { get; set; } = 1;

        [Column("CUSTOMER_ID", TypeName = "varchar"), MaxLength(36)]
        [ForeignKey("Customer")]
        public Guid CustomerId { get; set; }

        public Customer Customer { get; set; } = null!;

        public Notification? Notification { get; set; }

        public ICollection<OrderDetail> Details {get;} = new List<OrderDetail>();
    }
}
