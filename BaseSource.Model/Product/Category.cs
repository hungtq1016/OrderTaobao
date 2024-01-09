using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseSource.Model
{
    [Table("CATEGORIES")]
    public class Category : BaseEntity
    {

        [Column("NAME", TypeName = "nvarchar"), MaxLength(255)]
        public string? Name { get; set; } = string.Empty;

        [Column("SLUG", TypeName = "nvarchar"), MaxLength(255)]
        public string? Slug { get; set; } = string.Empty;

        public List<ProductCategory>? Products { get; } = new();

    }
}
