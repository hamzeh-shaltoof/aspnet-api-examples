using InMemoryCache.Data;
using InMemoryCache.Services;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddMemoryCache(options => options.SizeLimit = 100);

builder.Services.AddControllers().AddJsonOptions(options =>
       options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull);

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite("Data Source = app.db"));

var app = builder.Build();

app.MapControllers();

app.Run();
