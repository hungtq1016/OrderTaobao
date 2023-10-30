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
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddHttpContextAccessor();
            services.AddSingleton<IUriService>(o =>
            {
                var accessor = o.GetRequiredService<IHttpContextAccessor>();
                var request = accessor.HttpContext.Request;
                var uri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());
                return new UriService(uri);
            });
            return services;
        }
    }
}
