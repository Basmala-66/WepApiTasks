using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace Health_Care_System.Filters
{
    public class ActionTimingFilter(ILogger<ActionTimingFilter> logger) : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
           var stopWatch = Stopwatch.StartNew();

            try
            {
                await next();
            }
            finally
            {
                stopWatch.Stop();
                logger.LogInformation("MVC action {ActionName} took {ElapsedMilliseconds} ms",context.ActionDescriptor.DisplayName
                    ,stopWatch.Elapsed.Microseconds);
            }
        }
    }
}
