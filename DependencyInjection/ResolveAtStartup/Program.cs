var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<UserSeed>();

var app = builder.Build();


// Like HTTP Fack 
using(var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;

    var userSeed = serviceProvider.GetRequiredService<UserSeed>();

    userSeed.CreateUserData();

}

app.MapGet("/", () => "Hello World!");

app.Run();


public class UserSeed
{

    public readonly ILogger<UserSeed> _logger;

    public UserSeed(ILogger<UserSeed> logger)
    {
        _logger = logger;

    }

    public void CreateUserData()
    {
        _logger.LogInformation("Add Completed 1000 User ");
    }

}