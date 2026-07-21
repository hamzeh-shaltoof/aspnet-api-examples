using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
// DI (Ioc Container)
// Service Registration

           // This Is Way --> (Using LifeTime Extension Method)

//builder.Services.AddTransient<IWeatherService, WeatherService>();
//builder.Services.AddTransient<IWeatherClient, WeatherClient>();


             // This Is Way --> (Using LifeTime Extension Method)
builder.Services.AddWeatherServices(); // Grouping Of Services (Using LifeTime Extension Method) 


//builder.Services.Add(new ServiceDescriptor(
//     typeof(IWeatherService),
//     typeof(WeatherService),
//     ServiceLifetime.Transient
//    )); // Using With Service Descriptor

builder.Services.AddSomthing();




var app = builder.Build();

app.MapGet("/{cityName}", (string cityName , [FromServices] IWeatherService weatherService) =>
{
    return weatherService.GetWeatherInfo(cityName);
});

app.MapGet("single" ,( IEnumerable<ISomething> somethings) => {

    string response = " ";

    foreach (var something in somethings)
    {
        response += something.DoSomething();
    }

    return Results.Ok(response);
});

app.MapGet("GetCount", (IServiceProvider serviceProvider) =>
{
    var response = serviceProvider.GetServices<ISomething>();
    return response.Count();
});



app.Run();


public interface IWeatherService
{
    public string GetWeatherInfo(string CityName);
}
public class WeatherService : IWeatherService
{
    public IWeatherClient WeaterClient;

    public WeatherService(IWeatherClient WeaterClient)
    {
        this.WeaterClient = WeaterClient;
    }
    public string GetWeatherInfo(string CityName)
    {
        return WeaterClient.GetWeatherInfo(CityName);
    }
}
public interface IWeatherClient
{
    public string GetWeatherInfo(string CityName);
}
public class WeatherClient : IWeatherClient
{
    public string GetWeatherInfo(string CityName) {
        return $"{Random.Shared.Next(-10, 40)} C";
    }
}



public interface ISomething
{
    public string DoSomething();
}
public class SomethingV1 : ISomething
{
    public string DoSomething()
    {
        return "Something V1 ";
    }
}
public class SomethingV2 : ISomething
{
    public string DoSomething()
    {
        return "Something V2";
    }
}
