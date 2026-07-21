using M02.ModelAndInMemoryStoreSetupRazorPages.Store;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<ProductStore>();
builder.Services.AddRazorPages();
var app = builder.Build();

app.UseRouting();

app.MapRazorPages();

app.MapGet("/", () => "Hello World!");

app.Run();
