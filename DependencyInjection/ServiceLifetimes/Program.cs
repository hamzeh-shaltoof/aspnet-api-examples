var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ServiceA>();
builder.Services.AddScoped<ServiceB>();

builder.Services.AddSingleton<RequestTracker>();

var app = builder.Build();

app.MapGet("/request1", (ServiceA serviceA , ServiceB serviceB) =>
{
    return Results.Ok( 
        new{
            A = serviceA.GetInfo(),
            B = serviceB.GetInfo(),
        });
});
app.MapGet("/request2", (ServiceA serviceA , ServiceB serviceB) =>
{
    return Results.Ok( 
        new{
            A = serviceA.GetInfo(),
            B = serviceB.GetInfo(),
        });
});

app.Run();

public class RequestTracker() {
  public string TrackerId = Guid.NewGuid().ToString()[..8];
}
public class ServiceA(RequestTracker tracker) {
  public string GetInfo()
    {
        return tracker.TrackerId;
    }
}
public class ServiceB(RequestTracker tracker) {
  public string GetInfo()
    {
        return tracker.TrackerId;
    }
}
