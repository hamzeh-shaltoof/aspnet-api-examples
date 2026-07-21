var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


app.MapGet("/Gaza/{year}-{month}-{day}", (int year,int month,int day) 
    => $"Your Date -> {new DateOnly(year, month, day)}");

app.MapGet("/Gaza/{year}+{month}+{day}", (int year,int month,int day) 
    => $"Your Date -> {new DateOnly(year, month, day)}");

// This Is Default Value Route Parameter In Case Not Found Any URLs 
app.MapGet("/{controller=Home}/{id?}", (int? id) => id != null ? $"Users { id }" : "All User"); // Optional Value

app.MapGet("/a{b}c{d}", (string b, string d) => $"b: {b} d: {d}"); // Encode

app.MapGet("/single/{*slug}", (string slug) => $"slug: {slug}"); //  Single Stare

app.MapGet("/double/{**slug}", (string slug) => $"slug: {slug}"); // Double Stare

app.Run();
