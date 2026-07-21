
using Asp.Versioning.Builder;
using Microsoft.AspNetCore.Mvc;
using MinimalAPIQueryStringVersioning.Data;
using MinimalAPIQueryStringVersioning.Responses.V2;
using System.Threading.Tasks;

namespace MinimalAPIQueryStringVersioning.Endpoint.V2
{
    public static class ProductEndpoint
    {
        public static RouteGroupBuilder MapProductEndpointV2(this IEndpointRouteBuilder app, ApiVersionSet apiVersionSet)
        {
            var productApi = app.MapGroup("api/v{version:apiVersion}/products")
                                 .WithApiVersionSet(apiVersionSet)
                                 .HasApiVersion(new Asp.Versioning.ApiVersion(2,0));



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
            responseProduct = ProductResponse.FromModel(product!);



            return responseProduct;
        }

    }
}
