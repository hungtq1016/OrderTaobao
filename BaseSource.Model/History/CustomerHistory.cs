using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseSource.Model
{
    [Table("CUSTOMER_HISTORY")]
    public class CustomerHistory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID", TypeName = "varchar"), MaxLength(36)]
        public Guid Id { get; set; }

        [Column("OLD_VALUE", TypeName = "nvarchar")]
        public string OldValue { get; set; } = string.Empty;

        [Column("FIELD", TypeName = "nvarchar"), MaxLength(36)]
        public string Field { get; set; } = string.Empty;

        [Column("UPDATED_AT")]
        public DateTime UpdatedAt { get; set; }

        [Column("CUSTOMER_ID", TypeName = "varchar"), MaxLength(36)]
        [ForeignKey("Customer")]
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;

    }
}
