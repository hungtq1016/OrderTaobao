
namespace Microsoft.Extensions
{
    public static class OAuth2
    {
        public static IServiceCollection AddOAuth2(this IServiceCollection services, IConfigurationRoot configuration)
        {
            var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
            var policies = new Dictionary<string, string>
            {
                { "AdminView", "admin.view" },
                { "UserView", "user.view" },
                { "DeleteView", "delete.view" },
                { "UserDelete", "user.delete" },
                { "UserEdit", "user.edit" },
                { "RoleView", "role.view" }
            };

            services.AddAuthorization(options =>
            {
                foreach(var policy in policies)
                {
                    options.AddPolicy(policy.Key, builder =>
                    {
                        builder.RequireClaim("permission", policy.Value,"all");
                    });
                }

            });
            


            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
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
