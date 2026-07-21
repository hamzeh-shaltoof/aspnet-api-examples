using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ImplementingResourceFilters.Filters
{
    public class TenantValidationFilter(IConfiguration config) : IAsyncResourceFilter
    {

        public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
        {
            var tenantId = context.HttpContext.Request.Headers["tenantId"].ToString();
            var apiKey = context.HttpContext.Request.Headers["X-api-key"].ToString();

            var expectedTenantKey = config[$"Tenants:{tenantId}:ApiKey"];

            if (string.IsNullOrEmpty(expectedTenantKey) || expectedTenantKey != apiKey) {
                context.Result = new ForbidResult();
                return;
            }

            await next();
        }
    }
}
