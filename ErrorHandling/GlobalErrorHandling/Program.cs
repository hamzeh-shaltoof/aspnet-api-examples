using GlobalErrorHandling.Exceptions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddExceptionHandler<GlobalExceptionHandling>();

builder.Services.AddProblemDetails(options =>
{
    options.CustomizeProblemDetails = (context) =>
    { // Instance Is Where Error 
        context.ProblemDetails.Instance =$"{context.HttpContext.Request.Method} {context.HttpContext.Request.Path}";
        context.ProblemDetails.Extensions.Add("requestId", context.HttpContext.TraceIdentifier);
    };
}); ;

var app = builder.Build();

app.UseExceptionHandler();

app.UseStatusCodePages();

app.MapControllers();




app.MapGet("/", () => "Hello World!");

app.Run();
