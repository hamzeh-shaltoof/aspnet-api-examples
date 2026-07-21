var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/{cityName}", (string cityName) =>
{
    IWeatherClient weatherClient = new WeatherClient();
    IWeatherService weatherService= new WeatherService(weatherClient);

});

app.Run();
public interface IWeatherService 
{
    public string GetWeatherInfo(string cityName);
}

public class WeatherService : IWeatherService
{
    IWeatherClient weatherClient;
    public WeatherService(IWeatherClient weatherClient)
    {
        this.weatherClient = weatherClient;
    }
    public string GetWeatherInfo(string cityName)
    {
        return this.weatherClient.GetWeatherInfo(cityName);
    }
}

public interface IWeatherClient
{
    public string GetWeatherInfo(string cityName);

}
public class WeatherClient : IWeatherClient
{
    public string GetWeatherInfo(string cityName)
    {
        return $"{Random.Shared.Next(-10, 40)} C";
    }
}

