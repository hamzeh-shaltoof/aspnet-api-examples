using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ImplementingExceptionFilters.Filters
{
    
    public class GlobalExceptionFilter :  IAsyncExceptionFilter
    {
        public Task OnExceptionAsync(ExceptionContext context)
        {
            var problemDetails = new ProblemDetails()
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Internet Server Error",
                Detail = context.Exception.Message

            };

            context.Result = new ObjectResult(problemDetails)
            {
                StatusCode = problemDetails.Status,
            };

            context.ExceptionHandled = true; //  Exception Handle Completed 
            return Task.CompletedTask;
        }
    }
}
