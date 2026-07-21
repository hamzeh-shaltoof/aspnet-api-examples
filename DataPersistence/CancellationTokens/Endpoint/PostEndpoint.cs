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
    public static class PostEndpoint
    {
        public static RouteGroupBuilder MapPostGroup(this IEndpointRouteBuilder app)
        {
            var postApi = app.MapGroup("api/Post");

            postApi.MapPost("", CreateProduct);

            return postApi;
        }

        public static async Task<IResult> CreateProduct(CreateProductRequest request, IProductRepository repository, CancellationToken cancellationToken)
        {
            if (!await repository.ExistsByNameAsync(request.Name, cancellationToken))
            {
                var product = new Model.Product
                {
                    Id = Guid.NewGuid(),
                    Name = request.Name,
                    Price = request.Price
                };

                await repository.AddProductAsync(product, cancellationToken);

                return Results.CreatedAtRoute(
                    routeName: "GetProductById",
                    routeValues: new { productId = product.Id },
                    value: ProductResponse.FromModel(product)
                );
            }

            return Results.Conflict($"A Product With Name {request.Name} Already Exists.");
        }
    }
}