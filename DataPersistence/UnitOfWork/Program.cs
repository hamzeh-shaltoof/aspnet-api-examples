using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using UnitOfWork.Data;
using UnitOfWork.Endpoint;
using UnitOfWork.Interface;
using UnitOfWork.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IUnitOfWork, UnitOfWork.Repository.UnitOfWork>();

builder.Services.Configure<JsonOptions>(options =>
     options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull);

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlite("Data Source = app.db");
});

var app = builder.Build();



app.MapGet("/", () => "Hello World!");


app.MapAcceptedGroup();
app.MapDeleteGroup();
app.MapFileGroup();
app.MapProductGroup();
app.MapHeadGroup();
app.MapOptionsGroup();
app.MapPostGroup();
app.MapPutGroup();

app.Run();
