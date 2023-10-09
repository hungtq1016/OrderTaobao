
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BaseSource.Model
{
    [Table("PRODUCTS")]
    public class Product : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID", TypeName = "varchar"), MaxLength(36)]
        public Guid Id { get; set; }

        [Column("NAME", TypeName = "nvarchar"), MaxLength(255)]
        public string Name { get; set; } = string.Empty;

        [Column("SLUG", TypeName = "nvarchar"), MaxLength(255)]
        public string Slug { get; set; } = string.Empty;

        [Column("PRICE", TypeName = "nvarchar"), MaxLength(255)]
        public string Price { get; set; } = string.Empty;

        [Column("DESCRIPTION")]
        public string Description { get; set; } = string.Empty;

        [Column("QUANTITY")]
        public int Quantity { get; set; } = 0;

        [Column("IS_AVAILABLE")]
        public bool IsAvailable { get; set; } = false;
        [Column("ENABLE")]
        public bool Enable { get; set; } = true;

        [Column("CATEGORY_ID", TypeName = "varchar"), MaxLength(36)]
        [ForeignKey("Category")]
        public Guid CategoryId { get; set; }

        public Category Category { get; set; } = null!;
        public ICollection<OrderDetail> Orders { get; } = new List<OrderDetail>();

    }
}
