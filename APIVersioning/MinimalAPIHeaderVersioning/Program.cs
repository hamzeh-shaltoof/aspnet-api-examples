using Asp.Versioning;
using Microsoft.AspNetCore.Http.Json;
using MinimalAPIHeaderVersioning.Data;
using MinimalAPIQueryStringVersioning.Endpoint.V1;
using MinimalAPIQueryStringVersioning.Endpoint.V2;
using System.Text.Json.Serialization;


var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

builder.Services.AddSingleton<ProductRepository>();

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new Asp.Versioning.ApiVersion(1.0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
    options.ApiVersionReader = new HeaderApiVersionReader("api-version");
});

var app = builder.Build();

var apiVersionSet = app.NewApiVersionSet()
                       .HasApiVersion(new ApiVersion(1, 0))
                       .HasApiVersion(new ApiVersion(2, 0))
                       .ReportApiVersions()
                       .Build;

app.MapProductEndpointV1(apiVersionSet());
app.MapProductEndpointV2(apiVersionSet());

app.Run();
