namespace ProductService.Models
{
    public class Product : Entity
    {

        public string Name { get; set; } = string.Empty;

        public string Slug { get; set; } = string.Empty;

        public long Price { get; set; }

        public string Description { get; set; } = string.Empty;

        public int Quantity { get; set; }

    }
}
