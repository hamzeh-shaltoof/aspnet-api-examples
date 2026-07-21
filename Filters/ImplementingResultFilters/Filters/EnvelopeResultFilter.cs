using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ImplementingResultFilters.Filters
{
    public class EnvelopeResultFilter : IAsyncResultFilter
    {
        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            if (context.Result is ObjectResult result && result.Value != null) {

                var wrapped = new
                {
                    success = "true",
                    data = result.Value
                };
                context.Result = new JsonResult(wrapped)
                {
                    StatusCode = result.StatusCode,
                };
            }
        
                await next();
        }
    }
}
