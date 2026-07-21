using ImplementingActionFilters.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
{
    //options.Filters.Add<TrackActionTimeFilter>(); // Global Filter

});

var app = builder.Build();

app.MapControllers();


app.Run();
