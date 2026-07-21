using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
                 .AddXmlSerializerFormatters();


var app = builder.Build();

app.MapPost("/productBody", ([FromBody] Product product) => {
    return product;
});


app.MapControllers();

app.Run(); 

public class Product
{
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public int Quantity { get; set; }

}

