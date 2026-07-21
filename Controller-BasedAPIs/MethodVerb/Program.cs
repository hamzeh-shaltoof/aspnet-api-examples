using MethodVerb.Data;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<ProductRepository>();

builder.Services.AddControllers()
               .AddJsonOptions(option =>
               option.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull)
               .AddNewtonsoftJson(option => 
                 option.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore);

var app = builder.Build();

app.MapControllers();

app.Run();
