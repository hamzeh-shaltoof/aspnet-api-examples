using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UnitOfWork.Data;
using System;
using UnitOfWork.Interface;
using System.Threading.Tasks;

namespace UnitOfWork.Endpoint
{
    public static class HeadEndpoint
    {
        public static RouteGroupBuilder MapHeadGroup(this IEndpointRouteBuilder app)
        {
            var headApi = app.MapGroup("api/Head");

            headApi.MapMethods("{productId:guid}", [ "HEAD" ], HeadProduct);

            return headApi;
        }

        public static async Task<IResult> HeadProduct(Guid productId, IUnitOfWork unitOfWork , CancellationToken cancellationToken)
        {
            return await
                unitOfWork.repository.IsExistsByIdAsync(productId, cancellationToken) ? Results.Ok() : Results.NotFound();
        }
    }
}