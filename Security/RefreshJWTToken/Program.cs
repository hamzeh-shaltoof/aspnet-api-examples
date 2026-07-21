using RefreshJWTToken.Contracts;
using RefreshJWTToken.Permissions;
using RefreshJWTToken.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IJwtTokenProvider, JwtTokenProvider>();

builder.Services.AddControllers();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    var jwtSettings = builder.Configuration.GetSection("JwtSettings");

    options.TokenValidationParameters = new()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ClockSkew = TimeSpan.Zero,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]!))
    };
});

builder.Services.AddAuthorization(options =>
{
    // Project Management Permissions
    options.AddPolicy(Permission.Project.Create, policy => policy.RequireClaim("Permission", Permission.Project.Create));
    options.AddPolicy(Permission.Project.Read, policy => policy.RequireClaim("Permission", Permission.Project.Read));
    options.AddPolicy(Permission.Project.Update, policy => policy.RequireClaim("Permission", Permission.Project.Update));
    options.AddPolicy(Permission.Project.Delete, policy => policy.RequireClaim("Permission", Permission.Project.Delete));
    options.AddPolicy(Permission.Project.AssignMember, policy => policy.RequireClaim("Permission", Permission.Project.AssignMember));
    options.AddPolicy(Permission.Project.ManageBudget, policy => policy.RequireClaim("Permission", Permission.Project.ManageBudget));

    // Task Management Permissions (demonstrating granularity)
    options.AddPolicy(Permission.Task.Create, policy => policy.RequireClaim("Permission", Permission.Task.Create));
    options.AddPolicy(Permission.Task.Read, policy => policy.RequireClaim("Permission", Permission.Task.Read));
    options.AddPolicy(Permission.Task.Update, policy => policy.RequireClaim("Permission", Permission.Task.Update));
    options.AddPolicy(Permission.Task.Delete, policy => policy.RequireClaim("Permission", Permission.Task.Delete));
    options.AddPolicy(Permission.Task.AssignUser, policy => policy.RequireClaim("Permission", Permission.Task.AssignUser));
    options.AddPolicy(Permission.Task.Comment, policy => policy.RequireClaim("Permission", Permission.Task.Comment));
});
var app = builder.Build();



app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
