var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/authors/{author}", async (string author, HttpContext context) => {

    var data = new
    {
        Id = context.TraceIdentifier,
        Scheme = context.Request.Scheme,
        Host = context.Request.Host,
        Method = context.Request.Method,
        Query = context.Request.Query,
        RouteValues = context.Request.RouteValues,
        Header = context.Request.Headers,
        Path = context.Request.Path,
        Body = await new StreamReader(context.Request.Body).ReadToEndAsync(),


    };
    return Results.Ok(data);
});

app.Run();

