using Microsoft.EntityFrameworkCore;
using ResponseCaching.Data;
using ResponseCaching.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddResponseCaching();
builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite("Data Source = app.db"));
builder.Services.AddScoped<IProductService, ProductService>();

var app = builder.Build();

app.UseResponseCaching();
app.MapControllers();


app.MapGet("/api/products-mn", async (IProductService productService, int page = 1, int pageSize = 10) =>
{
    Console.WriteLine("Minimal Endpoint visited");

    var PagedResult = await productService.GetProductsAsync(page, pageSize);

    return Results.Ok(PagedResult);
});

app.MapGet("/api/products-mn/{productId:int}", async (
    int productId,
    IProductService productService) =>
{
    Console.WriteLine("Minimal Api (Get By Id) visited");

    var response = await productService.GetProductByIdAsync(productId);

    return response is null
        ? Results.NotFound($"Product with Id '{productId}' not found")
        : Results.Ok(response);
});


app.Run();
