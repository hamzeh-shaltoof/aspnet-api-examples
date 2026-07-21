using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/header", (
      [FromHeader(Name = "X-Version-Api")] decimal apiVersion 
    , [FromHeader] string k1 
    , [FromHeader] string k2) 

    => $"Version = {apiVersion}\n K1 = {k1}\n K2 = {k2}\n  ");

app.Run();
