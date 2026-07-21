
using Asp.Versioning;
using Asp.Versioning.Builder;
using Microsoft.AspNetCore.Mvc;
using MinimalAPIMediaTypeVersioning.Data;
using MinimalAPIMediaTypeVersioning.Responses.V2;

namespace MinimalAPIQueryStringVersioning.Endpoint.V2
{
    public static class ProductEndpoint
    {

        public static RouteGroupBuilder MapProductEndpointV2(this  IEndpointRouteBuilder app, ApiVersionSet apiVersionSet)
        {
          var  productApi = app.MapGroup("api/products")
                            .WithApiVersionSet(apiVersionSet)
                            .HasApiVersion(new ApiVersion(2, 0));

            productApi.MapGet("/{id:int}", GetProductById).WithName("GetProductByIdV2");

            return productApi;

        }

        private static ProductResponse GetProductById(
            HttpResponse response
            , ProductRepository repository
            , [FromRoute] int id
            , [FromQuery] bool isInclude = false)
        {


            var product = repository.GetProductById(id);
            ProductResponse responseProduct;
            if (isInclude)
            {
                var review = repository.GetReviewsByProductId(id);
                responseProduct = ProductResponse.FromModel(product!, review!);
                return responseProduct;
            }
            responseProduct = ProductResponse.FromModel(product! , null);



            return responseProduct;
        }
    }
}
