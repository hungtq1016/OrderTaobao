namespace Infrastructure.EFCore.Middlewares
{
    public class ResponseWrappingMiddleware
    {
        private readonly RequestDelegate _next;

        public ResponseWrappingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var originalBody = context.Response.Body;
            try
            {
                using (var memStream = new MemoryStream())
                {
                    context.Response.Body = memStream;

                    await _next(context);
                    var statusCode = context.Response.StatusCode;

                    memStream.Position = 0;
                    var responseBody = await new StreamReader(memStream).ReadToEndAsync();

                    var data = System.Text.Json.JsonSerializer.Deserialize<object>(responseBody);
                    var wrappedResponse = new Response<object>
                    {
                        Data = data,
                        StatusCode = statusCode,
                        Message = ((StatusMessageEnum)statusCode).ToString(),
                        IsError = statusCode < 200 || statusCode >= 300
                    };

                    var json = System.Text.Json.JsonSerializer.Serialize(wrappedResponse);
                    var buffer = Encoding.UTF8.GetBytes(json);

                    context.Response.Body = originalBody;
                    context.Response.Headers.Remove("Content-Length");
                    context.Response.ContentLength = buffer.Length;
                    await context.Response.Body.WriteAsync(buffer, 0, buffer.Length);
                }
            }
            finally
            {
                context.Response.Body = originalBody;
            }

        }
    }

}
