using Microsoft.AspNetCore.Mvc.Filters;

namespace ImplementingActionFilters.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class TrackActionTimeFilterV2 : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {


            context.HttpContext.Items["ActionTime"] = DateTime.UtcNow;

            Console.WriteLine("Sample Sync Before");

            await next();

            var startDate = (DateTime)context.HttpContext.Items["ActionTime"]!;

            var elapsed = (DateTime.UtcNow - startDate!);

            context.HttpContext.Response.Headers.Append("X-Elapsed-Time", $"Trace Action Time Filter Choke {elapsed.Microseconds}ms");

            Console.WriteLine("Sample Sync After");

        }
    }
}
