using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.EFCore.Extensions
{
    public static class MapperExtension
    {
        public static IServiceCollection AddCustomMapper<TMapper>(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(TMapper).Assembly);
            return services;
        }
    }
}
