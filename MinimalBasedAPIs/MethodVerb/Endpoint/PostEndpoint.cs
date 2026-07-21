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
    public static class PostEndpoint
    {
        public static RouteGroupBuilder MapPostGroup(this IEndpointRouteBuilder app)
        {
            var postApi = app.MapGroup("api/Post");

            postApi.MapPost("", CreateProduct);

            return postApi;
        }

        public static IResult CreateProduct(CreateProductRequest request, IProductRepository repository)
        {
            if (!repository.ExistsByName(request.Name))
            {
                var product = new Model.Product
                {
                    Id = Guid.NewGuid(),
                    Name = request.Name,
                    Price = request.Price
                };

                repository.AddProduct(product);

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