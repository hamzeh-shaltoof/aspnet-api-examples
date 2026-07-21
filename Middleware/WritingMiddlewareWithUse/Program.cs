var builder = WebApplication.CreateBuilder(args);

// DI Container ( Configuration Dependency)

var app = builder.Build();

// Middleware


app.Use((RequestDelegate next) => next);

app.Use((RequestDelegate next) =>
{
    return async (HttpContext context) =>
    {

        context.Response.StatusCode = 200; // This Line Is True Because Response Not Started
        await context.Response.WriteAsync("Middleware 2 "); // This Line Is Begin Response
        await next(context);
    };
});

app.Use(async (HttpContext context, RequestDelegate next) => {
    if (!context.Response.HasStarted)  // Guard If Response Not Started
    {
        context.Response.Headers.Append("Hdr0", "Val0");
    }
    await context.Response.WriteAsync("Middleware 3 "); // Change On Body (True) Because Body Is(Stream)
    await next(context);
});



//app.Use(async ( context,  next) => {
//    context.Response.StatusCode = StatusCodes.Status200OK; // Exception Because Response Started
//    context.Response.Headers.Append("X-Hdr1", "Val1");  // Exception Because Response Started
//    await next(context);
//});

//app.Use((RequestDelegate next) => {
//    return async (HttpContext context) => {
//        context.Response.StatusCode = StatusCodes.Status200OK; // Exception Because Response Started
//        context.Response.Headers.Append("X-Hdr1", "Val1"); // Exception Because Response Started
//        await next(context);
//    };
//});

app.Run(async (HttpContext context) => {
    await context.Response.WriteAsync("I'm Terminal Middleware"); // This Line Is Response Start And This Is Body
});

app.Run();
