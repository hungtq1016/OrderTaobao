
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseSource.Model
{
    [Table("PRODUCTS")]
    public class Product : BaseEntity
    {

        [Column("NAME", TypeName = "nvarchar"), MaxLength(255)]
        public string Name { get; set; } = string.Empty;

        [Column("SLUG", TypeName = "nvarchar"), MaxLength(255)]
        public string Slug { get; set; } = string.Empty;

        [Column("PRICE", TypeName = "nvarchar"), MaxLength(255)]
        public string Price { get; set; } = string.Empty;

        [Column("DESCRIPTION")]
        public string Description { get; set; } = string.Empty;

        [Column("QUANTITY")]
        public UInt16 Quantity { get; set; } = 0;

        [Column("IS_AVAILABLE")]
        public bool IsAvailable { get; set; } = false;

        [Column("CATEGORY_ID", TypeName = "varchar"), MaxLength(36)]
        [ForeignKey("Category")]
        public string CategoryId { get; set; }

        public Category Category { get; set; } = null!;
        public ICollection<OrderDetail> Orders { get; } = new List<OrderDetail>();

    }
}
