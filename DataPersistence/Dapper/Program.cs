using Dapper;
using DapperMicroOptimizations.Data;
using DapperMicroOptimizations.Handler;
using Microsoft.Data.Sqlite;
using System.Data;
using System.Reflection.Metadata;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ProductRepository>();

builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                });

builder.Services.AddScoped<IDbConnection>(_ => new SqliteConnection("Data Source = app.db") );

SqlMapper.AddTypeHandler(new GuidHandler());

var app = builder.Build();

using var scope = app.Services.CreateScope();

var db = scope.ServiceProvider.GetRequiredService<IDbConnection>();

DatabaseInitializer.Initialize(db);


app.MapControllers();

app.Run();
