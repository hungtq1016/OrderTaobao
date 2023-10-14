using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseSource.Model
{
    [Table("ACCESS_TOKEN")]
    public class AccessToken : BaseEntity
    {

        [Column("TOKEN")]
        public string Token { get; set; } = string.Empty;
        [Column("LAST_USED_AT")]
        public DateTime LastUsedAt { get; set; }
        [Column("EXPIRE_AT")]
        public DateTime ExpireAt { get; set; }

        [Column("CUSTOMER_ID", TypeName = "varchar"), MaxLength(36)]
        [ForeignKey("Customer")]
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;

    }
}
