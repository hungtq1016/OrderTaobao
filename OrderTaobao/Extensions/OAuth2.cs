
using BaseSource.BackendAPI.Authorization.Requirements;

namespace Microsoft.Extensions
{
    public static class OAuth2
    {
        public static IServiceCollection AddOAuth2(this IServiceCollection services, IConfigurationRoot configuration)
        {
            var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
         /*   var policies = new List<string>
                {"AdminView","UserView","DeleteView","UserDelete","UserEdit","RoleView"};

            services.AddAuthorization(options =>
            {
                foreach (var policy in policies)
                {
                    options.AddPolicy(policy, builder =>
                    {
                        builder.RequireClaim("permission", policy, "all");
                    });

                    options.AddPolicy(policy, builder =>
                        builder.Requirements.Add(new PermissionRequirement(policy)));
                }


            });*/

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
