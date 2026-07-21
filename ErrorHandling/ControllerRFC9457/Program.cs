var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddProblemDetails(); // Prepare Problem Details Standard

var app = builder.Build();

app.UseExceptionHandler(); // Capture Any Exception And Convert To Problem Details Standard

app.UseStatusCodePages(); // Capture Any Status Code And Convert To Problem Details Standard

if(app.Environment.IsDevelopment())
app.UseDeveloperExceptionPage(); // Send Stack Trace In Environment Developer Only

app.MapControllers();
app.Run();
