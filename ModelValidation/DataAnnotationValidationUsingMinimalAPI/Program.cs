using DataAnnotationValidationUsingMinimalAPI.Extensions;
using DataAnnotationValidationUsingMinimalAPI.Requests;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using static System.Runtime.InteropServices.JavaScript.JSType;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});


var app = builder.Build();

app.MapPost("api/products/create", (CreateProductRequest request) =>
{
    return Results.Created($"api/products/{Guid.NewGuid()}", request);
}).Validate<CreateProductRequest>(); // My Method Validate<>() In Folder Extensions

app.Run();
