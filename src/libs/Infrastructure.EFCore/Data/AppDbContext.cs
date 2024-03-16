namespace Infrastructure.EFCore.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options):base(options){}
        public AppDbContext(){}
    }
}
