using UnitOfWork.Data;
using UnitOfWork.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace UnitOfWork.Endpoint
{
    public static class DeleteEndpoint
    {

        public static RouteGroupBuilder MapDeleteGroup(this IEndpointRouteBuilder app)
        {

            var DeleteApi = app.MapGroup("api/delete");
            DeleteApi.MapDelete("{productId:guid}", Delete);

            return DeleteApi;
        }
        public static async Task<IResult> Delete(Guid productId , IUnitOfWork unitOfWork , CancellationToken cancellationToken)
        {
            var isFound = unitOfWork.repository.IsExistsByIdAsync(productId, cancellationToken);

            if (!await isFound)
                return Results.NotFound($"Not Found The Product With");

            var succeed = await unitOfWork.repository.DeleteProductAsync(productId, cancellationToken);
            if (!succeed)
            {
                await unitOfWork.SaveChangesAsync(cancellationToken);
                return Results.StatusCode(500);
            }

            return Results.NoContent();
        }
    }


}
