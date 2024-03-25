namespace ProductService.Data
{
    public class ProductContext : AppDbContext
    {
        public ProductContext(DbContextOptions options) : base(options) { }

        public DbSet<Product> Products => Set<Product>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ProductConfiguration());

        }
    }
}
