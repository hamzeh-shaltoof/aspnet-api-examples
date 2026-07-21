var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/{cityName}", (string cityName) =>
{
    var weatherService = new WeatherService();
    var weatherInfo = weatherService.GetWeatherInfo(cityName);
    return Results.Ok(weatherInfo);
});

app.Run();


public class WeatherService()
{
    public string GetWeatherInfo(string cityName)
    {
        var weatherClient = new WeatherClient();
        return weatherClient.GetWeatherInfo(cityName);

    }
}
public class WeatherClient()
{
    public string GetWeatherInfo(string cityName)
    {
        return $"{Random.Shared.Next(-10, 40)} C";

    }
}
