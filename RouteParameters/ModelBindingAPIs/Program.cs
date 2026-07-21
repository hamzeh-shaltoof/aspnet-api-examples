var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var app = builder.Build();


// Example Found In Folder Controllers/ProductController

app.MapControllers();

app.Run();
