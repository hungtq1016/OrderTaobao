using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BaseSource.Model
{
    [Table("CUSTOMER_ROLE")]
    public class UserRole : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID", TypeName = "varchar"), MaxLength(36)]
        public Guid Id { get; set; }

        [Column("CUSTOMER_ID", TypeName = "varchar"), MaxLength(36)]
        [ForeignKey("Customer")]
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;

        [Column("ROLE_ID", TypeName = "varchar"), MaxLength(36)]
        [ForeignKey("Role")]
        public Guid RoleId { get; set; }
        public Role Role { get; set; } = null!;

    }
}
