
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

        [Column("PRICE")]
        public UInt32 Price { get; set; }

        [Column("DESCRIPTION")]
        public string Description { get; set; } = string.Empty;

        [Column("QUANTITY")]
        public UInt16 Quantity { get; set; }

        public List<ProductCategory>? Categories { get; } = new();

        public List<OrderDetail>? Orders { get; } = new();

    }
}
