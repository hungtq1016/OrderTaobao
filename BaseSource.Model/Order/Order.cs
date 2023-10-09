using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BaseSource.Model
{
    [Table("ORDERS")]
    public class Order : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID", TypeName = "varchar"), MaxLength(36)]
        public Guid Id { get; set; }

        [Column("STATUS")]
        public int Status { get; set; } = 1;
        [Column("ENABLE")]
        public bool Enable { get; set; } = true;

        [Column("CUSTOMER_ID", TypeName = "varchar"), MaxLength(36)]
        [ForeignKey("Customer")]
        public Guid CustomerId { get; set; }

        public Customer Customer { get; set; } = null!;

        public Notification? Notification { get; set; }

        public ICollection<OrderDetail> Details {get;} = new List<OrderDetail>();
    }
}
