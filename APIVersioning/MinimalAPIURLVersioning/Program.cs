using Asp.Versioning;
using MinimalAPIQueryStringVersioning.Data;
using MinimalAPIQueryStringVersioning.Endpoint.V1;
using MinimalAPIQueryStringVersioning.Endpoint.V2;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<ProductRepository>();

builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                });

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
    options.ApiVersionReader = new UrlSegmentApiVersionReader();
});

var app = builder.Build();

var apiVersionSet = app.NewApiVersionSet()
                       .HasApiVersion(new ApiVersion(1))
                       .HasApiVersion(new ApiVersion(2))
                       .ReportApiVersions()
                       .Build;

app.MapControllers();
app.MapProductEndpointV1(apiVersionSet());
app.MapProductEndpointV2(apiVersionSet());

app.Run();