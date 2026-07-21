using RepositoryPattern.Data;
using RepositoryPattern.Interface;
using RepositoryPattern.Request;
using RepositoryPattern.Responses;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;

namespace RepositoryPattern.Endpoint
{
    public static class PutEndpoint
    {
        public static RouteGroupBuilder MapPutGroup(this IEndpointRouteBuilder app)
        {
            var putApi = app.MapGroup("api/Put");

            putApi.MapPut("{productId:guid}", UpdateProduct);

            return putApi;
        }

        public static IResult UpdateProduct(Guid productId, UpdateProductRequest request, IProductRepository repository)
        {
            var product = repository.GetProductById(productId);
            if (product is null)
            {
                return Results.NotFound("I'm Sorry , Not Found This Product ");
            }

            product.Name = request.Name;
            product.Price = request.Price;

            var successed = repository.UpdateProduct(product);

            if (!successed)
            {
                return Results.StatusCode(500);
            }

            return Results.CreatedAtRoute(
                routeName: "GetProductById",
                routeValues: new { productId = product.Id },
                value: ProductResponse.FromModel(product)
            );
        }
    }
}