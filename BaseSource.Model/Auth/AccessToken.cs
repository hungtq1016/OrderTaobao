using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BaseSource.Model
{
    [Table("ACCESS_TOKEN")]
    public class AccessToken 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID", TypeName = "varchar"), MaxLength(36)]
        public Guid Id { get; set; }

        [Column("TOKEN", TypeName = "varchar"), MaxLength(36)]
        public Guid Token { get; set; }
        [Column("LAST_USED_AT")]
        public DateTime LastUsedAt { get; set; }
        [Column("EXPIRE_AT")]
        public DateTime ExpireAt { get; set; }

        public bool IsVerify { get; set; } = false;

        [Column("CUSTOMER_ID", TypeName = "varchar"), MaxLength(36)]
        [ForeignKey("Customer")]
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;

        [Column("CREATED_AT")]
        public DateTime CreatedAt { get; set; }
    }
}
