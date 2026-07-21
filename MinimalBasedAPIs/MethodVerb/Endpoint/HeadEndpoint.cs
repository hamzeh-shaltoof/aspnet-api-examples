using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using RepositoryPattern.Data;
using System;
using RepositoryPattern.Interface;

namespace RepositoryPattern.Endpoint
{
    public static class HeadEndpoint
    {
        public static RouteGroupBuilder MapHeadGroup(this IEndpointRouteBuilder app)
        {
            var headApi = app.MapGroup("api/Head");

            headApi.MapMethods("{productId:guid}", [ "HEAD" ], HeadProduct);

            return headApi;
        }

        public static IResult HeadProduct(Guid productId, IProductRepository repository)
        {
            return repository.IsExistsById(productId) ? Results.Ok() : Results.NotFound();
        }
    }
}