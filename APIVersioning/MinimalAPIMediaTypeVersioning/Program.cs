using Asp.Versioning;
using Asp.Versioning.Builder;
using Microsoft.AspNetCore.Http.Json;
using MinimalAPIMediaTypeVersioning.Data;
using MinimalAPIQueryStringVersioning.Endpoint.V1;
using MinimalAPIQueryStringVersioning.Endpoint.V2;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<ProductRepository>();

builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault;
});

builder.Services.AddApiVersioning(options => {
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
    options.ApiVersionReader = new MediaTypeApiVersionReader("v");
});

var app = builder.Build();

var apiVersionSet = app.NewApiVersionSet()
                        .HasApiVersion(new ApiVersion(1))
                        .HasApiVersion(new ApiVersion(2))
                        .Build;

app.MapProductEndpointV1(apiVersionSet());
app.MapProductEndpointV2(apiVersionSet());

app.Run();
