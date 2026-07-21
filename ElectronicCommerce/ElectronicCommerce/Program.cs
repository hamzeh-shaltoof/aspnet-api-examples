using ElectronicCommerce.Data;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();



app.MapGet("/", () =>
{
    using (var context = new AppDbContext())
    {
        var users = context.Users.ToList();
        return users;
    }
});

app.Run();
