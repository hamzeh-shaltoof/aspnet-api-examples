using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using CancellationTokens.Data;
using System;
using CancellationTokens.Interface;
using System.Threading.Tasks;

namespace CancellationTokens.Endpoint
{
    public static class HeadEndpoint
    {
        public static RouteGroupBuilder MapHeadGroup(this IEndpointRouteBuilder app)
        {
            var headApi = app.MapGroup("api/Head");

            headApi.MapMethods("{productId:guid}", [ "HEAD" ], HeadProduct);

            return headApi;
        }

        public static async Task<IResult> HeadProduct(Guid productId, IProductRepository repository , CancellationToken cancellationToken)
        {
            return await
                repository.IsExistsByIdAsync(productId, cancellationToken) ? Results.Ok() : Results.NotFound();
        }
    }
}