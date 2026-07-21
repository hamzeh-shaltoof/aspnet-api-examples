using CancellationTokens.Data;
using CancellationTokens.Interface;
using CancellationTokens.Request;
using CancellationTokens.Responses;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Threading.Tasks;

namespace CancellationTokens.Endpoint
{
    public static class PutEndpoint
    {
        public static RouteGroupBuilder MapPutGroup(this IEndpointRouteBuilder app)
        {
            var putApi = app.MapGroup("api/Put");

            putApi.MapPut("{productId:guid}", UpdateProduct);

            return putApi;
        }

        public static async Task<IResult> UpdateProduct(
            Guid productId,
            UpdateProductRequest request,
            IProductRepository repository,
            CancellationToken cancellationToken)
        {
            var product = await repository.GetProductByIdAsync(productId, cancellationToken);
            if (product is null)
            {
                return Results.NotFound("I'm Sorry , Not Found This Product ");
            }

            product.Name = request.Name;
            product.Price = request.Price;

            var successed = repository.UpdateProductAsync(product, cancellationToken);

            if (!await successed)
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