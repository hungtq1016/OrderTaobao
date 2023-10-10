using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

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
        public Guid? ParentId { get; set; }
        public Category Parent { get; set; } = null!;

        public ICollection<Category> Children { get;} = new List<Category>();
    }
}
