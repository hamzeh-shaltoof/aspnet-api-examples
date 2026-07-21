using CancellationTokens.Data;
using CancellationTokens.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CancellationTokens.Endpoint
{
    public static class DeleteEndpoint
    {

        public static RouteGroupBuilder MapDeleteGroup(this IEndpointRouteBuilder app)
        {

            var DeleteApi = app.MapGroup("api/delete");
            DeleteApi.MapDelete("{productId:guid}", Delete);

            return DeleteApi;
        }
        public static async Task<IResult> Delete(Guid productId , IProductRepository repository , CancellationToken cancellationToken)
        {
            var isFound = repository.IsExistsByIdAsync(productId, cancellationToken);

            if (!await isFound)
                return Results.NotFound($"Not Found The Product With");

            var succeed = await repository.DeleteProductAsync(productId, cancellationToken);
            if (!succeed)
                return Results.StatusCode(500);

            return Results.NoContent();
        }
    }


}
