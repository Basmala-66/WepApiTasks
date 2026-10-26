
using System.Diagnostics;

namespace Health_Care_System.Middlewares
{
    public class RequestTimingMiddleware(ILogger<RequestTimingMiddleware> logger) : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var stopWatch = Stopwatch.StartNew();
            await next(context);

            stopWatch.Stop();

            logger.LogInformation($"Request {context.Request.Method} {context.Request.Path} took {stopWatch.ElapsedMilliseconds} ms");
        }
    }
}
