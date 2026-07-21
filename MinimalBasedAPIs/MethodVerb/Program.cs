using Dapper;
using DapperMicroOptimizations.Handler;
using RepositoryPattern.Data;
using RepositoryPattern.Endpoint;
using RepositoryPattern.Interface;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Reflection.Metadata;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddScoped<IProductRepository, EFProductRepository>();
builder.Services.AddScoped<IProductRepository, DapperProductRepository>();

builder.Services.Configure<JsonOptions>(options =>
    options.SerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
);

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlite("Data Source = app.db");
});
builder.Services.AddScoped<IDbConnection>(_ => new SqliteConnection("Data Source = app.db"));

SqlMapper.AddTypeHandler(new GuidHandler());
Dapper.SqlMapper.AddTypeMap(typeof(double), System.Data.DbType.Double);


var app = builder.Build();


app.MapAcceptedGroup();
app.MapDeleteGroup();
app.MapFileGroup();
app.MapProductGroup();
app.MapHeadGroup();
app.MapOptionsGroup();
//app.MapPatchGroup();
app.MapPostGroup();
app.MapPutGroup();

app.Run();
