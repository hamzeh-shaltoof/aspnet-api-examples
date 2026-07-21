var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();



// This Is Way --> (Using LifeTime Extension Method)
// Grouping Of Services (Using LifeTime Extension Method) 
builder.Services.AddWeatherServices(); 


builder.Services.AddSomthing();

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
