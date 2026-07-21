
using Asp.Versioning;
using Asp.Versioning.Builder;
using Microsoft.AspNetCore.Mvc;
using MinimalAPIURLVersioning.Data;
using MinimalAPIURLVersioning.Responses.V1;

namespace MinimalAPIQueryStringVersioning.Endpoint.V1
{
    public static class ProductEndpoint
    {

        public static RouteGroupBuilder MapProductEndpointV1(this  IEndpointRouteBuilder app, ApiVersionSet apiVersionSet)
        {


            var  productApi = app.MapGroup("api/products")
                                 .WithApiVersionSet(apiVersionSet)
                                 .HasApiVersion(new ApiVersion(1, 0));

            productApi.MapGet("/{id:int}", GetProductById).WithName("GetProductByIdV1");

            return productApi;

        }

        private static ProductResponse GetProductById(
            HttpResponse response
            , ProductRepository repository
            , [FromRoute] int id
            , [FromQuery] bool isInclude = false)
        {
            response.Headers["Deprecation"] = "true";
            response.Headers["Sunset"] = "WED , OCT 7/10/2026  12:00:00";
            response.Headers["link"] = "<api/v2/products>; rel = \"successor-version\" ";

            var product = repository.GetProductById(id);
            ProductResponse responseProduct;
            if (isInclude)
            {
                var review = repository.GetReviewsByProductId(id);
                responseProduct = ProductResponse.FromModel(product!, review!);
                return responseProduct;
            }
            responseProduct = ProductResponse.FromModel(product!);



            return responseProduct;
        }
    }
}
