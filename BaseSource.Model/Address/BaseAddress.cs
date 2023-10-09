using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BaseSource.Model
{
    public abstract class BaseAddress : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID", TypeName = "varchar"), MaxLength(36)]
        public Guid Id { get; set; }
        [Column("NAME", TypeName = "nvarchar"), MaxLength(36)]
        public string Name { get; set; } = string.Empty;

        [Column("SLUG", TypeName = "nvarchar"), MaxLength(36)]
        public string Slug { get; set; } = string.Empty;

        [Column("TYPE", TypeName = "nvarchar"), MaxLength(36)]
        public string Type { get; set; } = string.Empty;

        [Column("TYPE_SLUG", TypeName = "nvarchar"), MaxLength(36)]
        public string TypeSlug { get; set; } = string.Empty;
        [Column("CODE")]
        public int Code { get; set; } 
    }
}
