using HybridCache.Data;
using HybridCache.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Hybrid;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();




// Distributed Cache Is  SqlServerCache And RedisCache , So Will Be Stord On Of Them (Sql Or Redis)

//builder.Services.AddStackExchangeRedisCache(options =>
//{
//    options.Configuration = builder.Configuration["ConnectionStrings:Redis"]; // This Is = Localhost:6379
//    options.InstanceName = "HybridCache:"; // After ':' Will Be keyNameCache 
//});

builder.Services.AddDistributedSqlServerCache(options =>
{
    options.ConnectionString = builder.Configuration.GetConnectionString("SqlCache"); // This Is To Connection In Sql Server 
    options.SchemaName = "dbo";
    options.TableName = "CacheEntries"; // This Is Table Name That Will Store CachData In Database 
});

builder.Services.AddHybridCache(options =>
{
    options.DefaultEntryOptions = new HybridCacheEntryOptions()
    {
        Expiration = TimeSpan.FromMinutes(10), // Layer 3 And Layer 2 => ( L2,L3 ) All Them Be Distributed Cache
        LocalCacheExpiration = TimeSpan.FromSeconds(30) // L1
    };
});

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite("Data Source = app.db"));
builder.Services.AddScoped<IProductService,ProductService>();
var app = builder.Build();

app.MapControllers();

app.Run();
