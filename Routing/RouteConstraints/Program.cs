using RouteConstraints.RouteConstraints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRouting(options => {
    options.ConstraintMap.Add("MyValidDate", typeof(RouteConstraintsDate));
    });

var app = builder.Build();

app.MapGet("/products/{id}", (int id) => "Hello World!");

app.MapGet("/users/{id:int}", (int id) => "Hello World!");

app.MapGet("/datetime/{dob:datetime}", (DateTime dob)
    => $"DateTime: {dob}");

app.MapGet("/decimal/{price:decimal}", (decimal price)
    => $"Decimal: {price}");

app.MapGet("/double/{weight:double}", (double weight)
    => $"Double: {weight}");

app.MapGet("/float/{weight:float}", (float weight)
    => $"Float: {weight}");

app.MapGet("/guid/{id:guid}", (Guid id)
    => $"GUID: {id}");

app.MapGet("/long/{ticks:long}", (long ticks)
    => $"Long: {ticks}");





app.MapGet("/length/{filename:length(12)}", (string filename)
    => $"Exact Length(12): {filename}");

app.MapGet("/lengthrange/{filename:length(8,16)}", (string filename)
    => $"Length(8-16): {filename}");

app.MapGet("/minlength/{username:minlength(4)}", (string username)
    => $"MinLength(4): {username}");

app.MapGet("/maxlength/{filename:maxlength(8)}", (string filename)
    => $"MaxLength(8): {filename}");

app.MapGet("/min/{age:min(18)}", (int age)
    => $"min(18): {age}");




app.MapGet("/alpha/{name:alpha}", (string name)
    => $"Alpha: {name}");


app.MapGet(@"/regex/{ssn:regex(^\d{{3}}-\d{{2}}-\d{{4}}$)}", (string ssn)
    => $"Regex Match (SSN): {ssn}");


app.MapGet("/required/{name:required}", (string name)
    => $"Required: {name}");

app.MapGet("/year-month-day/{year:MyValidDate}/{month}/{day}", (int year,int month,int day) 
    => $"DateOnly: {new DateOnly(year, month, day)}");
app.Run();
