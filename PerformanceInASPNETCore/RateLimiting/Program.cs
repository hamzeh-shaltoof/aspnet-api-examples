using RateLimiting.Data;
using RateLimiting.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddRateLimiter(static options =>
{
    options.AddFixedWindowLimiter("DefaultPolicy", limiterOptions =>
    {
        limiterOptions.Window = TimeSpan.FromMinutes(1);
        limiterOptions.PermitLimit = 50;
        // PermitLimit = Per 1 Minute Will Allows 100 Request Only 

        limiterOptions.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        // Storage After 100 Request Will Be Queue And When Finished Any Request Will Enter Oldest Request

        limiterOptions.QueueLimit = 10;
        // After 100 Request ,Will Storage 10 Request Only (10 Request Will Wait (Blocked)) and Otherwise return Response (429) => 'To Many Request'
    });

    options.AddSlidingWindowLimiter("SlidingWindow", limiterOptions => {
        limiterOptions.Window = TimeSpan.FromMinutes(1);
        limiterOptions.PermitLimit = 100;
        limiterOptions.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        limiterOptions.QueueLimit = 10;
        limiterOptions.SegmentsPerWindow = 6;
        // Divides 1-Minute Window Into 6 Moving 10-Second Segments.

        limiterOptions.AutoReplenishment = true;
        // Automatically Refills Available Request Permits As Time Moves Forward.
    });

    // This Is To Heavy Request Such As Big Query Or PDF Or Videw Or Image . . . All Of Them Same Moment
    options.AddConcurrencyLimiter("Concurrency", limiterOptions =>
    {
        limiterOptions.PermitLimit = 50;
        // Max 50 Active Requests Processing At The Exact Same Moment

        // Processes Queued Requests In Chronological Order (FIFO)
        limiterOptions.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;

        // Holds Up to 100 Extra Requests When Limit Is Reached
        limiterOptions.QueueLimit = 100;
    });

    // This Is Policy On All User 
    options.AddPolicy("ApiUserPolicy", httpContext =>
        RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: httpContext.User.Identity?.Name ?? "anonymous",
            factory: _ =>
            {
                return new FixedWindowRateLimiterOptions
                {
                    Window = TimeSpan.FromMinutes(1),
                    PermitLimit = 100,
                    AutoReplenishment = true

                };
            }));

    // This Is Policy On All IP 
    options.AddPolicy("ApiIpPolicy", (httpContext) =>
        RateLimitPartition.GetSlidingWindowLimiter(
            partitionKey: httpContext.Connection.RemoteIpAddress?.ToString() ?? "anonymous",
            factory: _ =>
            {
                return new SlidingWindowRateLimiterOptions()
                {
                    Window = TimeSpan.FromMinutes(1),
                    PermitLimit = 100,
                    AutoReplenishment = true
                };
            }));

});

builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlite("Data Source = app.db");
});


var app = builder.Build();

app.UseRateLimiter();

app.MapControllers();


app.MapGet("/api/products-mn", async (IProductService productService, int page = 1, int pageSize = 10) =>
{
    Console.WriteLine("Minimal Endpoint visited");

    var PagedResult = await productService.GetProductsAsync(page, pageSize);

    return Results.Ok(PagedResult);
}).RequireRateLimiting("DefaultPolicy");

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
