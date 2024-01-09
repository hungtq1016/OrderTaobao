
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BaseSource.Model
{
    [Table("PRODUCT_CATEGORY")]
    public class ProductCategory
    {
        [Column("PRODUCT_ID", TypeName = "varchar"), MaxLength(36)]
        [Key]
        public string ProductId { get; set; } = string.Empty;

        [Column("CATEGORY_ID", TypeName = "varchar"), MaxLength(36)]
        [Key]
        public string CategoryId { get; set; }= string.Empty;

        public Product? Product { get; set; }

        public Category? Category { get; set; }
    }
}
