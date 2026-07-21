using LoggingConfiguration.Services;
using Microsoft.Extensions.Logging.Console;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.SetMinimumLevel(LogLevel.Critical); // All Provider And All (Default,Microsoft,Micorsoft.Hosting.Lifetime)

builder.Logging.AddFilter("Microsoft.Hosting.Lifetime",LogLevel.Information);
builder.Logging.AddFilter("Microsoft",LogLevel.Error);

builder.Logging.AddFilter<ConsoleLoggerProvider>((category, level) =>
{
    if (category is not null) {
        if (category.StartsWith("microsoft", StringComparison.OrdinalIgnoreCase))
            return level > LogLevel.Critical;

        if (category.StartsWith("LoggingConfiguration.Services", StringComparison.OrdinalIgnoreCase))
            return level > LogLevel.Error;


    }
    return level >= LogLevel.Trace;
});
builder.Services.AddControllers();

builder.Services.AddScoped<IOrderService, OrderService>();


var app = builder.Build();

app.MapControllers();


app.Run();
