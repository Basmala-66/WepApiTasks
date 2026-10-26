using Health_Care_System.Errors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace Health_Care_System.Filters
{
    public class ActionExecutionLoggingFilter(ILogger<ActionExecutionLoggingFilter> logger) : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {

            var actionName = context.ActionDescriptor.DisplayName;
            if (context.Exception is not null && !context.ExceptionHandled)
            {
                logger.LogError(context.Exception, "MVC action { ActionName}failed", actionName);
                return;
            }

            logger.LogInformation("Finishing MVC action {ActionName}", actionName);
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            logger.LogInformation("Starting MVC action {ActionName}", context.ActionDescriptor.DisplayName);
        }
    }

    public class ExceptionHandlingFilter(ILogger<ExceptionHandlingFilter> logger, IHostEnvironment environment) : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if(context.ExceptionHandled)
                return;

            logger.LogError(context.Exception,
                  "Unhandled exception in MVC action {ActionName} for request {TraceId}",
                  context.ActionDescriptor.DisplayName,
                  context.HttpContext.TraceIdentifier);

            var problem = ExceptionProblemDetailsFactory.Create(context.Exception, context.HttpContext, environment);

            var result = new ObjectResult(problem)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };

            result.ContentTypes.Add("application/problem+json");

            context.Result = result;
            context.ExceptionHandled = true;
        }
    }
}
