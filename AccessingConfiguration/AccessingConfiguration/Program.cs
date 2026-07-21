var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/get-value-by-key", (IConfiguration config) =>
{
    return config["ServiceName"];
});

app.MapGet("/get-value-by-path", (IConfiguration config) =>
{
    return config["ConnectionStrings:DefaultConnection"];
});

app.MapGet("/get-value-by-connection-string", (IConfiguration config) =>
{
    return config.GetConnectionString("DefaultConnection");
});

// should be single key (key:value) , not hierarchy key (key : key:value)
app.MapGet("/get-value-by-get-value", (IConfiguration config) => { 
    return config.GetValue<string>("ServiceName");
});

// create object
app.MapGet("/get-object",(IConfiguration config) => {
    return config.GetSection(AppSettings.Name).Get<AppSettings>();  
});

app.MapGet("/get-object-bind",(IConfiguration config) => {

    AppSettings appsettings = new();
    config.GetSection(AppSettings.Name).Bind(appsettings);
    return appsettings;

} );
app.Run();

public class AppSettings()
{
    public const string Name = "AppSettings";
    public TimeSpan OpenAt {  get; set; }            
    public TimeSpan CloseAt { get; set; }
    public TimeSpan DaysOpen { get; set; }
    public bool EnableOnlineBooking { get; set; }
    public int MaxDailyAppointments { get; set; }

}
