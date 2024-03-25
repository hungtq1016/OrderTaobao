namespace ProductService.Infrastructure.Data
{
    public class ProductContextFactory : AppDbContextFactory<ProductContext>
    {
        public ProductContextFactory() : base("productDB.docker")
        {
        }
    }
}
