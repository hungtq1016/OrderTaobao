
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BaseSource.Model
{
    [Table("ROLES")]
    public class Role : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID", TypeName = "varchar"), MaxLength(36)]
        public Guid Id { get; set; }

        [Column("NAME", TypeName = "nvarchar"), MaxLength(36)]
        public string Name { get; set; } = string.Empty;

        [Column("ENABLE")]
        public bool Enable { get; set; } = true;
        public ICollection<UserRole> UserRoles { get; } = new List<UserRole>();
    }
}
