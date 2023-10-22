using Microsoft.AspNetCore.Identity;

namespace Microsoft.Extensions
{
    public static class Data
    {
        public static IServiceCollection AddDataContext(this IServiceCollection services)
        {
            services.AddDbContext<DataContext>();
            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<DataContext>()
                .AddDefaultTokenProviders();
            return services;
        }
    }
}
