using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var app = builder.Build();

//app.MapControllers();

//app.MapGet("Route-Table", (IServiceProvider serviceProvider) =>
//{
//    var endpoints = serviceProvider.GetRequiredService<EndpointDataSource>()
//                                   .Endpoints.Select(endpoint => endpoint.DisplayName);

//    return Results.Ok(endpoints);
//});


app.UseRouting();
#pragma warning disable ASP0014 // Suggest using top level route registrations
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapGet("/Route-Table", (IServiceProvider serviceProvider) =>
    {
        var endpoint = serviceProvider.GetRequiredService<EndpointDataSource>()
                                      .Endpoints.Select(ep => ep.DisplayName);
        return Results.Ok(endpoint);
    });

    endpoints.MapGet("/Welcome", () => "Hello Word ASP.NET CORE");
}); // ??? ??? ???? 
#pragma warning restore ASP0014 // Suggest using top level route registrations
app.Run();
