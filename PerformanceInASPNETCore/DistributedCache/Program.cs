using DistributedCache.Data;
using DistributedCache.Services;
using Microsoft.EntityFrameworkCore;
using System.Net.Sockets;
using System.Xml.Linq;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
    options.InstanceName = "DistributedCacheLearn";
});

builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite("Data Source = app.db"));

builder.Services.AddControllers();

var app = builder.Build();



app.MapGet("/", () => "Hello World!");

app.MapControllers();
app.Run();
