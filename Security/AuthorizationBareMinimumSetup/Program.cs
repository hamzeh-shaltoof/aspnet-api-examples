using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("user-with-dirve-taxi", policy =>
    {
        policy.RequireRole("user");
        policy.RequireClaim("drive","taxi");
    });
});

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/login", async (HttpContext httpContext) =>
{
    List<Claim> claims = [
        new("sub",Guid.NewGuid().ToString()),
        new ("name","hamzeh"),
        new ("email","hamzeh@localhost"),
        new (ClaimTypes.Role,"admin"),
        new (ClaimTypes.Role,"supervisor"),
        new ("drive","bus"),
        new ( "drive","taxi"),
        ];
    //CookieAuthenticationDefaults.AuthenticationScheme = "Cookies"
    ClaimsIdentity identity = new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);
    ClaimsPrincipal principal = new ClaimsPrincipal(identity);

   await httpContext.SignInAsync(
        scheme: CookieAuthenticationDefaults.AuthenticationScheme,
        principal: principal);

});

app.MapGet("/logout", (HttpContext httpContext) => httpContext.SignOutAsync());

app.MapGet("/admin", [Authorize(Roles = "admin")] (HttpContext httpContext) =>
{
    var user = httpContext.User;
    var userInformation = user.Claims.Select(c => new { c.Type, c.Value });

    return Results.Ok(userInformation);

});
app.MapGet("/supervisor", [Authorize(Roles = "supervisor")] (HttpContext httpContext) =>
{
    var user = httpContext.User;
    var userInformation = user.Claims.Select(c => new { c.Type, c.Value });

    return Results.Ok(userInformation);

});
app.MapGet("/admin-supervisor", (HttpContext httpContext) =>
{
    var user = httpContext.User;
    var userInformation = user.Claims.Select(c => new { c.Type, c.Value });

    return Results.Ok(userInformation);

}).RequireAuthorization(x => x.RequireRole("admin", "supervisor"));

app.MapGet("/drive-bus", (HttpContext httpContext) =>
{
    var user = httpContext.User;
    var userInformation = user.Claims.Select(c => new { c.Type, c.Value });

    return Results.Ok(userInformation);

}).RequireAuthorization(x => x.RequireClaim("dive","bus", "dive","taxi"));

app.MapGet("/drive-taxi", (HttpContext httpContext) =>
{
    var user = httpContext.User;
    var userInformation = user.Claims.Select(c => new { c.Type, c.Value });

    return Results.Ok(userInformation);

}).RequireAuthorization(x => x.RequireClaim("dive","bus", "dive","taxi"));

app.MapGet("/drive-bus-taxi", (HttpContext httpContext) =>
{
    var user = httpContext.User;
    var userInformation = user.Claims.Select(c => new { c.Type, c.Value });

    return Results.Ok(userInformation);

}).RequireAuthorization(x => x.RequireClaim("dive","bus", "dive","taxi"));

app.MapGet("/policy", (HttpContext httpContext) =>
{
    var user = httpContext.User;
    var userInformation = user.Claims.Select(c => new { c.Type, c.Value });

    return Results.Ok(userInformation);

}).RequireAuthorization("user-with-dirve-taxi");

app.MapGet("/Account/Login", () => "Login Page");
app.Run();
