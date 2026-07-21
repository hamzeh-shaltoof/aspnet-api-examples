
namespace ImplementingFiltersInMinimalAPIs.Filters
{
    public class TrackActionTimeFilter : IEndpointFilter
    {
        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            var startTime = DateTime.UtcNow;
            
          var reuslt =await next(context);

            var endTime = DateTime.UtcNow;
            var elapsed = endTime - startTime;

            context.HttpContext.Response.Headers.Append("X-Elapsed", $"{elapsed.Microseconds}ms");
            return reuslt;

        }
    }
}
