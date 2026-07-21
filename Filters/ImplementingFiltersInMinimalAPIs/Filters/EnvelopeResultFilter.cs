namespace ImplementingFiltersInMinimalAPIs.Filters
{
    public class EnvelopeResultFilter : IEndpointFilter
    {
        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            var result = await next(context);

            var wrapped = Results.Json(new
            {
                success = "true",
                data = result
            });
            return wrapped;
        }
    }
}
