using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Infrastructure.EFCore.Helpers;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.EFCore.Data
{
    public abstract class AppDbContextFactory<TDbContext> : IDesignTimeDbContextFactory<TDbContext> where TDbContext : DbContext
    {
        private readonly string _dbName;

        public AppDbContextFactory(string dbName)
        {
            _dbName = dbName;
        }

        public TDbContext CreateDbContext(string[] args)
        {
            var connectionString = ConfigurationHelper.GetConfiguration(AppContext.BaseDirectory)
                ?.GetConnectionString(_dbName);

            Console.WriteLine($"Connectiong string: {connectionString}");

            var optionsBuilder = new DbContextOptionsBuilder<TDbContext>()
                .UseSqlServer(
                    connectionString ?? throw new InvalidOperationException(),
                    sqlOptions =>
                    {
                        sqlOptions.MigrationsAssembly(GetType().Assembly.FullName);
                        sqlOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(30), null);
                    }
                );

            return (TDbContext)Activator.CreateInstance(typeof(TDbContext), optionsBuilder.Options);
        }
    }
}
