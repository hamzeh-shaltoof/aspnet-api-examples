var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var app = builder.Build();

app.MapGet("/", (HttpContext httpContext) =>
{
    var theme = httpContext.Request.Cookies["theme"];
    var language = httpContext.Request.Cookies["language"];
    var timezone = httpContext.Request.Cookies["timezone"];

    return Results.Ok(new
    {
        theme = theme,
        language = language,
        timezone = timezone
    });
});
 
app.MapControllers();

app.Run();
