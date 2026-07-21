var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("order/{id:int}", (int id,LinkGenerator link , HttpContext context) =>
{
    var editURL = link.GetUriByName("EditOrder", new { id }, context.Request.Scheme, context.Request.Host);
    var deleteURL = link.GetUriByName("DeleteOrder", new {id},context.Request.Scheme, context.Request.Host);
    var detailsURL = link.GetUriByName("DetailsOrder", new { id }, context.Request.Scheme, context.Request.Host);

    return Results.Ok(new
    {
        Id = id,
        Status = "PENDING",
        _link = new
        {
            self = new { href = context.Request.Path },
            edit = new {href = editURL , method = "PUT"},
            delete = new {href = deleteURL, method = "DELETE" },
            details = new {href = detailsURL , method = "GET"},
        }
    });
});


app.MapPut("order/{id:int}", () => "Completed Edit Order").WithName("EditOrder");
app.MapDelete("order/{id:int}", () => "Completed Delete Order").WithName("DeleteOrder");
app.MapGet("orderItem/{id:int}", () => "Completed Details Order ").WithName("DetailsOrder");
app.Run();
