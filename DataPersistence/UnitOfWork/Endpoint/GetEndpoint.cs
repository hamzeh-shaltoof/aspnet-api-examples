using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnitOfWork.Data;
using UnitOfWork.Interface;
using UnitOfWork.Model;
using UnitOfWork.Repository;
using UnitOfWork.Responses;

public static class GetEndpoint
{
    public static RouteGroupBuilder MapProductGroup(this IEndpointRouteBuilder app)
    {
        var productApi = app.MapGroup("api/Get");

        productApi.MapGet("{productId:guid}", GetProductById).WithName("GetProductById");
        productApi.MapGet("", GetPage);

        return productApi;
    }

    public static async Task<IResult> GetProductById(
        Guid productId,
        IUnitOfWork unitOfWork,
        CancellationToken cancellationToken,
        [FromQuery] bool isIncludedReviews = false)

    {
        var product = await unitOfWork.repository.GetProductByIdAsync(productId, cancellationToken);

        if (product == null)
        {
            return Results.NotFound(new { Message = "Product not found" });
        }

        if (!isIncludedReviews)
        {
            var response = ProductResponse.FromModel(product);
            return Results.Ok(response);
        }

        List<ProductReview> reviews = await unitOfWork.repository.GetReviewsByProductIdAsync(productId, cancellationToken);
        var fullResponse = ProductResponse.FromModel(product, reviews);

        return Results.Ok(fullResponse);
    }

    public static async Task<IResult> GetPage(
        IUnitOfWork unitOfWork,
        CancellationToken cancellationToken,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] bool isIncludedReviews = false)
    {
        page = Math.Max(1, page);
        pageSize = Math.Clamp(pageSize, 1, 100);

        var products = await unitOfWork.repository.GetProductPageAsync(page, pageSize, cancellationToken);
        var countProducts = await unitOfWork.repository.CountProductsAsync(cancellationToken);

        IEnumerable<ProductResponse> productsResponse;
        PaginationResponse<ProductResponse> paginationResponse;

        if (isIncludedReviews)
        {
            var reviews = await unitOfWork.repository.GetProductReviewsAsync(cancellationToken);
            productsResponse =  ProductResponse.FromModel(products, reviews);

            paginationResponse = PaginationResponse<ProductResponse>
                .Create(productsResponse, pageSize, countProducts, page);

            return Results.Ok(paginationResponse);
        }

        productsResponse = ProductResponse.FromModel(products);
        paginationResponse = PaginationResponse<ProductResponse>
            .Create(productsResponse, pageSize, countProducts, page);

        return Results.Ok(paginationResponse);
    }
}