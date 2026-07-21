using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OutputCaching.Data;
using OutputCaching.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddOutputCache(options =>
{
    options.DefaultExpirationTimeSpan = TimeSpan.FromMinutes(1);
    options.MaximumBodySize = 1024 * 64; // 64KB  Per Response
    options.SizeLimit = 1024 * 1024 * 100; // 100MG All Responses
    options.UseCaseSensitivePaths = false;
    options.AddPolicy("Single-Product", builder =>
    {
        builder.SetVaryByRouteValue(["productId"]).Expire(TimeSpan.FromMinutes(1));
        builder.Tag(["products"]);
    });
    options.AddPolicy("Pagination", builder =>
    {
        builder.SetVaryByQuery(["page", "pageSize"]).Expire(TimeSpan.FromMinutes(1));
        builder.Tag(["products"]);
    });
});

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite("Data Source = app.db"));

builder.Services.AddScoped<IProductService,ProductService>();

var app = builder.Build();

app.UseOutputCache();

app.MapControllers();

var group = app.MapGroup("/api/products-mn");

group.MapGet("/", async (IProductService productService, int page = 1, int pageSize = 10) =>
{
    Console.WriteLine("Minimal Endpoint visited");

    var PagedResult = await productService.GetProductsAsync(page, pageSize);

    return Results.Ok(PagedResult);
}).CacheOutput("Pagination");
    //.CacheOutput(options => options.Expire(TimeSpan.FromMinutes(1)).SetVaryByQuery(["page","pageSize"]));

group.MapGet("/{productId:int}", async (
    int productId,
    IProductService productService) =>
{
    Console.WriteLine("Minimal Api (Get By Id) visited");

    var response = await productService.GetProductByIdAsync(productId);

    return response is null
        ? Results.NotFound($"Product with Id '{productId}' not found")
        : Results.Ok(response);
}).CacheOutput("Single-Product");
    //.CacheOutput(options => options.Expire(TimeSpan.FromMinutes(1)).SetVaryByRouteValue(["productId"]));


app.Run();
