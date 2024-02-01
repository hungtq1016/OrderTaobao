using Infrastructure.EFCore.Repository;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Infrastructure.EFCore.Extensions
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddRepository(this IServiceCollection services, Type repoType)
        {
            var assembly = Assembly.GetAssembly(repoType); // Get the assembly containing your repository types

            foreach (var type in assembly.GetTypes())
            {
                if (type.IsClass && !type.IsAbstract && type.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRepository<>)))
                {
                    services.AddScoped(type); // Register the IRepository<>
                }
            }
            return services;
        }
    }
}
