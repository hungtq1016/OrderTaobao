using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseSource.Model
{
    [Table("CATEGORIES")]
    public class Category : BaseEntity
    {

        [Column("NAME", TypeName = "nvarchar"), MaxLength(255)]
        public string Name { get; set; } = string.Empty;

        [Column("SLUG", TypeName = "nvarchar"), MaxLength(255)]
        public string Slug { get; set; } = string.Empty;

        [Column("PARENT_ID", TypeName = "varchar"), MaxLength(36)]
        [ForeignKey("Parent")]
        public string? ParentId { get; set; }
        public Category Parent { get; set; } = null!;

        public ICollection<Category> Children { get; } = new List<Category>();
    }
}
