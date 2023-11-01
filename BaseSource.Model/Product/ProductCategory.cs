
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BaseSource.Model
{
    [Table("PRODUCT_CATEGORY")]
    public class ProductCategory
    {
        [Column("PRODUCT_ID", TypeName = "varchar"), MaxLength(36)]
        public string ProductId { get; set; }

        [Column("CATEGORY_ID", TypeName = "varchar"), MaxLength(36)]
        public string CategoryId { get; set; }

        public Product? Product { get; set; }

        public Category? Category { get; set; }
    }
}
