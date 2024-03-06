using System.Security.Claims;
using System.Text;

namespace OAuth2Service.Middlewares
{
    public class AuditTrailMiddleware
    {
        /*private const string ControllerKey = "controller";
        private readonly RequestDelegate _next;
        public AuditTrailMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, DataContext dbContext)
        {
            var request = context.Request;
            var body = await ReadBodyFromRequest(request);
            await _next(context);
            if (request.Path.StartsWithSegments("/api"))
            {
                request.RouteValues.TryGetValue(ControllerKey, out var controllerValue);
                var controllerName = (string)(controllerValue ?? string.Empty);

                List<string> values = new List<string> { "Authenticate", "Authorize" };

                if (!values.Contains(controllerName))
                    await AuditLogHandlerAsync(request, context, dbContext, controllerName, body);
            }
        }

        private async Task AuditLogHandlerAsync(HttpRequest request, HttpContext httpContext, DataContext dbContext, string controller,string body)
        {
            string? ipV4 = httpContext.Connection.RemoteIpAddress.ToString();

            switch (request.Method)
            {
                case "POST":
                case "PUT":
                case "PATCH":
                case "DELETE":
                    var auditLog = new AuditTrailHistory
                    {
                        Id = Guid.NewGuid().ToString(),
                        UserId = httpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value,
                        TableName = controller,
                        Action = request.Method,
                        EntityId = (request.Method == "DELETE" || request.Method == "PATCH" || request.Method == "PUT") ? GetQueryFromContext(httpContext) : null,
                        DeviceIP = ipV4,
                        StatusCode = httpContext.Response.StatusCode,   
                        Value = body
                    };

                    dbContext.AuditLogs.Add(auditLog);
                    await dbContext.SaveChangesAsync();
                    break;
                default:
                    break;
            }
        }

        private string GetQueryFromContext(HttpContext httpContext)
        {
            var routeData = httpContext.GetRouteData();
            var idFromContext = routeData.Values["id"];

            return idFromContext.ToString() ?? null;
        }

        private async Task<string> ReadBodyFromRequest(HttpRequest request)
        {
            if (!request.Body.CanSeek)
            {
                request.EnableBuffering();
            }

            request.Body.Position = 0;

            var reader = new StreamReader(request.Body, Encoding.UTF8);

            var body = await reader.ReadToEndAsync().ConfigureAwait(false);

            request.Body.Position = 0;

            return body;
        }*/
    }
}