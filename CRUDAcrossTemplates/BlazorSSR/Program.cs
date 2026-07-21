using BlazorSSR.Components.Pages;
using BlazorSSR.Store;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
                 .AddInteractiveServerComponents();  // SSR + SnigleR

builder.Services.AddSingleton<ProductStore>();

var app = builder.Build();

app.UseRouting();

app.UseAntiforgery();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();
     

app.MapGet("/", () => "Hello World!");

app.Run();
