using UnitOfWork.Data;
using UnitOfWork.Interface;
using UnitOfWork.Request;
using UnitOfWork.Responses;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace UnitOfWork.Endpoint
{
    public static class PostEndpoint
    {
        public static RouteGroupBuilder MapPostGroup(this IEndpointRouteBuilder app)
        {
            var postApi = app.MapGroup("api/Post");

            postApi.MapPost("", CreateProduct);
            postApi.MapPost("/review", CreateProductReview);

            return postApi;
        }

        private static async Task<IResult> CreateProductReview(
           [FromBody] CreateProductReview request,
            IUnitOfWork unitOfWork, 
            CancellationToken cancellationToken)
        {

            if (request is null)
                return Results.NotFound("Not succeeded Send Review");

            var productReview = new Model.ProductReview
            {
                Id = Guid.NewGuid(),
                ProductId = request.ProductId,
                Reviewer = request.Reviewer,
                Stars = request.Stars
            };

            await unitOfWork.repository.AddReviewAsync(productReview, cancellationToken);

            await unitOfWork.SaveChangesAsync(cancellationToken);
            return Results.Ok(new { Message = "Review added successfully", ReviewId = productReview.Id });

        }

        public static async Task<IResult> CreateProduct(CreateProductRequest request, IUnitOfWork unitOfWork, CancellationToken cancellationToken)
        {
            if (!await unitOfWork.repository.ExistsByNameAsync(request.Name, cancellationToken))
            {
                var product = new Model.Product
                {
                    Id = Guid.NewGuid(),
                    Name = request.Name,
                    Price = request.Price
                };

                await unitOfWork.repository.AddProductAsync(product, cancellationToken);
                await unitOfWork.SaveChangesAsync(cancellationToken);
               
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