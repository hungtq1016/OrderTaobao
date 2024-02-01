using Infrastructure.EFCore.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace Infrastructure.EFCore.Extensions
{
    public static class HttpStatusExtension
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
