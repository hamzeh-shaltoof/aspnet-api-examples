var builder = WebApplication.CreateBuilder(args);


builder.Services.AddKeyedTransient<ISomething, SomethingV1>("V1");
builder.Services.AddKeyedTransient<ISomething, SomethingV2>("V2");

var app = builder.Build();



app.MapGet("v1", ([FromKeyedServices("V1")] ISomething something) =>
{
    return something.DoSomething();
});

app.MapGet("v2", ([FromKeyedServices("V2")] ISomething something) =>
{
    return something.DoSomething();

});
app.Run();



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
