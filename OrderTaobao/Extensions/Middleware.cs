using BaseSource.BackendAPI.Middlewares;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class Middleware
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
