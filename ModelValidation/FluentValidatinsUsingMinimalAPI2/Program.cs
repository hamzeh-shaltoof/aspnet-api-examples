using FluentValidatinsUsingMinimalAPI.Requests;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Http.Json;

using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddValidatorsFromAssemblyContaining<Program>();

var app = builder.Build();

app.MapPost("api/products/create-product", ([FromBody] CreateProductRequest request) =>
{
    return Results.Created($"api/products/{Guid.NewGuid()}", request);
});

app.Run();
