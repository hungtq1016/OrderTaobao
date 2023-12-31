﻿namespace Microsoft.Extensions.DependencyInjection
{
    public static class DIService
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IService<>), typeof(Service<>));
            services.AddScoped<IAuthenticateService, AuthenticateService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IMailService, MailService>();
            services.AddScoped<IAuthHistoryService, AuthHistoryService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<IRoleService, RoleService>();    
            services.AddSingleton<IUriService, UriService>();
            services.AddHttpContextAccessor();
            
            return services;
        }
    }
}
