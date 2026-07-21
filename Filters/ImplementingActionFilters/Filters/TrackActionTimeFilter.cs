using Microsoft.AspNetCore.Mvc.Filters;

namespace ImplementingActionFilters.Filters
{
    public class TrackActionTimeFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {


            context.HttpContext.Items["ActionTime"] = DateTime.UtcNow;

            Console.WriteLine("Sample Sync Before");
            Console.WriteLine(context.HttpContext.Items["ActionTime"]);

            await next();

            var startDate = (DateTime)context.HttpContext.Items["ActionTime"]!;

            var elapsed = (DateTime.UtcNow - startDate!);

            context.HttpContext.Response.Headers.Append("X-Elapsed-Time", $"Trace Action Time Filter Choke {elapsed.Microseconds}ms");

            Console.WriteLine("Sample Sync After");

        }
    }
}
