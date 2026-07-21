using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace RepositoryPattern.Endpoint
{
    public static class OptionsEndpoint
    {
        public static RouteGroupBuilder MapOptionsGroup(this IEndpointRouteBuilder app)
        {
            var optionsApi = app.MapGroup("api/options");

            optionsApi.MapMethods("", ["OPTIONS"], GetOptions);

            return optionsApi;
        }

        public static IResult GetOptions(HttpContext context)
        {
            context.Response.Headers.Append("Allow", "OPTIONS , HEAD , GET , POST , PUT , PATCH , DELETE");

            return Results.NoContent();
        }
    }
}