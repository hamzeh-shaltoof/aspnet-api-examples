using MethodVerb.Data;
using MethodVerb.Interface;
using Microsoft.AspNetCore.Mvc;

namespace MethodVerb.Endpoint
{
    public static class DeleteEndpoint
    {

        public static RouteGroupBuilder MapDeleteGroup(this IEndpointRouteBuilder app)
        {

            var DeleteApi = app.MapGroup("api/delete");
            DeleteApi.MapDelete("{productId:guid}", Delete);

            return DeleteApi;
        }
        public static IResult Delete(Guid productId , IProductRepository repository)
        {
            var isFound = repository.IsExistsById(productId);

            if (!isFound)
                return Results.NotFound($"Not Found The Product With");

            var succeed = repository.DeleteProduct(productId);
            if (!succeed)
                return Results.StatusCode(500);

            return Results.NoContent();
        }
    }


}
