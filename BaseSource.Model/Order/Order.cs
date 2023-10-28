using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseSource.Model
{
    [Table("ORDERS")]
    public class Order : BaseEntity
    {

        [Column("STATUS")]
        public Byte Status { get; set; } = 1;

        [Column("USER_ID", TypeName = "nvarchar"), MaxLength(450)]
        [ForeignKey("User")]
        public string UserId { get; set; }
        public User User { get; set; } 

        public Notification? Notification { get; set; }

        public ICollection<OrderDetail> Details { get; } = new List<OrderDetail>();
    }
}
