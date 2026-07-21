var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers(option => option.ReturnHttpNotAcceptable = true) // 406
                .AddXmlSerializerFormatters();

var app = builder.Build();

app.MapControllers();

app.Run();
