using System.Net;

namespace OAuth2Service.Middlewares
{
    /*public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Something went wrong: {ex}");
                await HandleExceptionAsync(httpContext);
            };
        }

        private Task HandleExceptionAsync(HttpContext httpContext)
        {
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            httpContext.Response.ContentType = "application/json";

            return httpContext.Response.WriteAsync(new Response<bool>
            {
                StatusCode = (ushort)httpContext.Response.StatusCode,
                Data = false,
                Error = true,
                Message = "Internal Server Error!"
            }.ToString()!);
        }
    }*/
}
