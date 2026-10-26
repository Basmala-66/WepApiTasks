using Microsoft.AspNetCore.Mvc;

namespace Health_Care_System.Errors
{
    public class ExceptionProblemDetailsFactory
    {
        public static ProblemDetails Create(Exception exception,HttpContext context,IHostEnvironment environment)
        {
            var problem = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "An unexpected erroe occured",
                Detail = environment.IsDevelopment()?exception.Message:"please try later",Instance = context.Request.Path
            };

            problem.Extensions["traceId"] = context.TraceIdentifier;

            return problem;
        }
    }
}
