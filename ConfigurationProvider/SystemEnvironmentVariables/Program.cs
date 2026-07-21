var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddIniFile("config.ini", optional: false, reloadOnChange: true);
builder.Configuration.AddJsonFile("myconfighamzeh.json", optional : false , reloadOnChange : true);

var app = builder.Build();

app.MapGet("/{key}", (string key , IConfiguration config) =>
{
    return config[key];
});

app.Run();
