
using Azure.Core;

namespace Health_Care_System.Middlewares
{
    public sealed class RequestIdHeaderMiddleware(ILogger<RequestIdHeaderMiddleware> logger) : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
          var requestId = context.TraceIdentifier;

            context.Response.Headers["X-Request-Id"] = requestId;


            logger.LogDebug($"Assigned request ID {requestId} to {context.Request.Method} {context.Request.Path}");

            await next(context);
        }
    }
}
