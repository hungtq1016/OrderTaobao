namespace Microsoft.Extensions.DependencyInjection
{
    public static class DIService
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticateService, AuthenticateService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IMailService, MailService>();
            services.AddScoped<IAuthHistoryService, AuthHistoryService>();
            return services;
        }
    }
}
