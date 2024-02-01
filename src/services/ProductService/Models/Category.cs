using Core;

namespace ProductService.Models
{
    public class Category : Entity
    {

        public string? Name { get; set; } = string.Empty;

        public string? Slug { get; set; } = string.Empty;

        public List<ProductCategory>? Products { get; } = new();

    }
}
