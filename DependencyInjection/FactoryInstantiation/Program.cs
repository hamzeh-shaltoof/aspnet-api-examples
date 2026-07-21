var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<IPaymentProvider>(x =>
{
    var config = x.GetRequiredService<IConfiguration>();
    var provider = config["paymentProvider"];

    return provider == "StripePayment" ? new StripePayment() : new PayPalPayment();
});
var app = builder.Build();

app.MapGet("payment/{amount}", ( decimal amount , IPaymentProvider paymentProvider) =>{
    return paymentProvider.Pay(amount);
});

app.Run();


public interface IPaymentProvider
{
    public string Pay(decimal amount);
}

public class StripePayment : IPaymentProvider
{
    public string Pay(decimal amount)
    {
        return $"StripePayment - {amount:c}";
    }
}
public class PayPalPayment : IPaymentProvider
{
    public string Pay(decimal amount)
    {
        return $"PayPalPayment - {amount:c}";
    }
}