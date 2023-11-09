namespace Microsoft.Extensions
{
    public static class OAuth2
    {
        public static IServiceCollection AddOAuth2(this IServiceCollection services, IConfigurationRoot configuration)
        {
            services.AddAuthorization();
            services.AddCors(options =>
            {
               options.AddPolicy(name: "_myAllowSpecificOrigins",
                builder =>
                   {
                     builder.WithOrigins("*")
                     .AllowAnyMethod() // defining the allowed HTTP method
                     .AllowAnyHeader();
                   });
            });
            return services;
        }
    }
}
