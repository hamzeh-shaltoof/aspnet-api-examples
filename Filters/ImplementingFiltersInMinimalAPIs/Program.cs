using ImplementingFiltersInMinimalAPIs.Filters;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


app.MapGet("/api/products",() => {
    return Results.Ok(new { Name = "Keyboard", Price = 50m });
}).AddEndpointFilter<TrackActionTimeFilter>()
  .AddEndpointFilter<EnvelopeResultFilter>();

app.Run();
