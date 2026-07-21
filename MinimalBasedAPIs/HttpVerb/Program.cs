using InitialControllerSetup.Data;
using InitialControllerSetup.Responses;
using Microsoft.AspNetCore.Http.HttpResults;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<ProductRepository>();

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.DefaultIgnoreCondition =
        System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
});

var app = builder.Build();

app.MapMethods("/options", ["OPTIONS"] ,
    Results<Ok<ProductResponse>,NotFound>(ProductRepository repository) => {
    var product = repository.GetAllProducts().FirstOrDefault();

        if (product is null)
            return TypedResults.NotFound();

    var response = ProductResponse.FromModel(product);
        if (response is null)
            return TypedResults.NotFound();


        return TypedResults.Ok(response);

 } );

app.Run();
