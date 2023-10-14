namespace Microsoft.Extensions.DependencyInjection
{
    public static class DIRepository
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticateRepository, AuthenticateRepository>();
            /*services.AddScoped(typeof(IRepository<>), typeof(Repository<>));*/
            return services;
        }
    }
}
