using GroupedEndpoints.EndPoints;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapProductEndPoint();

app.Run();
