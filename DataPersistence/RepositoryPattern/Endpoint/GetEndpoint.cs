using MethodVerb.Data;
using MethodVerb.Interface;
using MethodVerb.Model;
using MethodVerb.Responses;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;

public static class GetEndpoint
{
    public static RouteGroupBuilder MapProductGroup(this IEndpointRouteBuilder app)
    {
        var productApi = app.MapGroup("api/Get");

        productApi.MapGet("{productId:guid}", GetProductById).WithName("GetProductById");
        productApi.MapGet("", GetPage);

        return productApi;
    }

    public static IResult GetProductById(
        Guid productId,
        IProductRepository repository,
        [FromQuery] bool isIncludedReviews = false)
    {
        var product = repository.GetProductById(productId);

        if (product == null)
        {
            return Results.NotFound(new { Message = "Product not found" });
        }

        if (!isIncludedReviews)
        {
            var response = ProductResponse.FromModel(product);
            return Results.Ok(response);
        }

        List<ProductReview> reviews = repository.GetReviewsByProductId(productId);
        var fullResponse = ProductResponse.FromModel(product, reviews);

        return Results.Ok(fullResponse);
    }

    public static IResult GetPage(
        IProductRepository repository,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] bool isIncludedReviews = false)
    {
        page = Math.Max(1, page);
        pageSize = Math.Clamp(pageSize, 1, 100);

        var products = repository.GetProductPage(page, pageSize);
        var countProducts = repository.CountProducts;

        IEnumerable<ProductResponse> productsResponse;
        PaginationResponse<ProductResponse> paginationResponse;

        if (isIncludedReviews)
        {
            var reviews = repository.GetProductReviews();
            productsResponse = ProductResponse.FromModel(products, reviews);

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