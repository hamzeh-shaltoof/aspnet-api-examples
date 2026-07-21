using MiddlewareBranching;

var builder = WebApplication.CreateBuilder(args);



var app = builder.Build();


app.Map("/branch1", Middleware.Branch1);
app.Map("/branch2", Middleware.Branch2);


// app.MapWhen(HttpContext => Condition , implementation If Condition Ture)
app.MapWhen((HttpContext context) =>
 context.Request.Path.Equals("/checkouts", StringComparison.OrdinalIgnoreCase) &&
 context.Request.Query["mode"] == "now"
 , branch => branch.Run(async (HttpContext context) =>
 {
     await context.Response.WriteAsync("Final");
 }));



app.Run();
