using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.Configure<AppSettings>(builder.Configuration.GetSection(AppSettings.Name));

builder.Services.AddOptions<AppSettings>().Bind(builder.Configuration.GetSection(AppSettings.Name));
var app = builder.Build();

// Singleton SnapShot 
app.MapGet("/ioption", (IOptions<AppSettings> option) => {
   return option.Value;
});

// SnapShot per Request
app.MapGet("/ioption-per-request", (IOptionsSnapshot<AppSettings> option) => {
   return option.Value;
});

// SnapShot per on change (fresh on change)
app.MapGet("/ioption-per-change", (IOptionsMonitor<AppSettings> option) => {
   return option.CurrentValue;
});


app.Run();

public class AppSettings()
{
    public const string Name = "AppSettings";
    public TimeSpan OpenAt { get; set; }
    public TimeSpan CloseAt { get; set; }
    public TimeSpan DaysOpen { get; set; }
    public bool EnableOnlineBooking { get; set; }
    public int MaxDailyAppointments { get; set; }

}
