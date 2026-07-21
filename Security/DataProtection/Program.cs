using DataProtection.Data;
using DataProtection.Services;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IBidingService, BidingService>();
builder.Services.AddDataProtection()
                .PersistKeysToDbContext<AppDbContext>();

builder.Services.AddControllers()
       .AddJsonOptions(options =>
          options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull);

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite("Data Source = app.db"));


var app = builder.Build();

app.MapControllers();

app.Run();
