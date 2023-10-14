﻿using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Microsoft.Extensions
{
    public static class OAuth2
    {
        public static IServiceCollection AddOAuth2(this IServiceCollection services, IConfigurationRoot configuration)
        {
            var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
            
            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly",
                    policy => policy.RequireClaim("Customer", "IT")
                                    .RequireRole("Customer"));
                options.AddPolicy("AuthUsers", policy => policy.RequireAuthenticatedUser());

            });

            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                    builder =>
                                    {
                                        builder.WithOrigins("http://localhost:3000",
                                                            "https://localhost:3000")
                                        .WithMethods("GET", "POST", "PUT", "PATCH", "DELETE") // defining the allowed HTTP method
                                        .AllowAnyHeader(); ;
                                    });
            });
            return services;
        }
    }
}
