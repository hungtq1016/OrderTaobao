using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseSource.Model
{
    [Table("AUTH_HISTORY")]
    public class AuthHistory : BaseEntity
    {

        [Column("CONTENT", TypeName = "nvarchar"), MaxLength(100)]
        public string Content { get; set; } = string.Empty;

        [Column("CUSTOMER_ID", TypeName = "varchar"), MaxLength(36)]
        [ForeignKey("Customer")]
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;

    }
}
