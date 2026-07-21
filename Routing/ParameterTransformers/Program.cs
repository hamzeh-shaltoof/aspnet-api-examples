using ParameterTransformers.Transformers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRouting(options =>
{
    options.ConstraintMap["slugify"] = typeof(SlugifyTransformer);
    //options.ConstraintMap.Add("slugify", typeof(SlugifyTransformer));
});

var app = builder.Build();

app.MapGet("/book/{title:slugify}", (string content) => content).WithName("BookTitle");

app.MapGet("/LinkGenerator", (LinkGenerator link , HttpContext context) =>
{
   var URL = link.GetPathByName("BookTitle", new { title = "Clean Code This Author Hamzeh Marwan Shaltouf" });
    return Results.Ok(new { title = URL }); 
    // This Is Path   "title": "/book/Clean%20Code%20This%20Author%20Hamzeh%20Marwan%20Shaltouf"

    // %20 This Is Space " " 

});

app.Run();
