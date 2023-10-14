
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace BaseSource.Model
{
    [Table("EMAIL_VERIFY")]
    public class EmailVerify : BaseEntity
    {
        [Column("VERIFY_AT")]
        public DateTime VerifyAt { get; set; }
        [Column("IS_VERIFY")]
        public bool IsVerify { get; set; } = false;

        [Column("CUSTOMER_ID", TypeName = "varchar"), MaxLength(36)]
        [ForeignKey("Customer")]
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;

    }
}
