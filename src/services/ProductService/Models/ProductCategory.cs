using Core;

namespace ProductService.Models
{
    public class ProductCategory : Entity
    {
        public string ProductId { get; set; } = string.Empty;

        public string CategoryId { get; set; }= string.Empty;

        public Product? Product { get; set; }

        public Category? Category { get; set; }
    }
}
