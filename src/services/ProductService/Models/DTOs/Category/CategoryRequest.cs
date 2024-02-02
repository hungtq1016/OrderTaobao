using Core;

namespace ProductService.DTOs
{
    public class CategoryRequest : EntityRequest
    {
        public string? Name { get; set; }
        public string? Slug { get; set; }
        public bool? Enable { get; set; } = true;
    }
}
