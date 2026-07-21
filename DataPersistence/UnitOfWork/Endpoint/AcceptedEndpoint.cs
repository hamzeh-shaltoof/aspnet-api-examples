using Microsoft.AspNetCore.Mvc;

namespace UnitOfWork.Endpoint
{

    public static class AcceptedEndpoint 
    {

        public static RouteGroupBuilder MapAcceptedGroup(this IEndpointRouteBuilder app)
        {

            var AcceptApi =  app.MapGroup("api/Accepted");
            AcceptApi.MapGet("", Accepted);
            AcceptApi.MapGet("status/{id:guid}", GetStatusProcessing);

            return AcceptApi;
        }
         private static IResult Accepted()
        {
            var id = Guid.NewGuid();

            return Results.Accepted($"/api/Accepted/status/{id}",new { Id = id, StatusCode = "Processing" });
        }

        [HttpGet("status/{id:guid}")]

        public static IResult GetStatusProcessing(Guid id, CancellationToken cancellationToken)
        {
           var isStillProcessing = false; // fake it 
            return Results.Ok(new { id, status = isStillProcessing ? "Processing" : "Completed" });
        }
    }
}
