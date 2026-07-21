using Microsoft.AspNetCore.Http.HttpResults;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var app = builder.Build();


app.MapPost("upload-minimal", async (IFormFile file) => {

    if (file is null || file.Length == 0)
        return Results.BadRequest("file not found");


    var uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
        Directory.CreateDirectory(uploadsPath);

    var filePath = Path.Combine(uploadsPath, file.FileName);

    using (var stream = new FileStream(filePath, FileMode.Create))
    {
        await file.CopyToAsync(stream);
    }

    return Results.Ok(new
    {
        Message = "File saved successfully!",
        file.FileName,
        file.Length,
        SavedPath = filePath
    });

}).DisableAntiforgery();











//app.MapPost("/upload", async (IFormFile file) =>
//{
//    if (file is null || file.Length == 0)
//        return Results.BadRequest("Not Found File");

//    var uploads = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
//    Directory.CreateDirectory(uploads);

//    var path = Path.Combine(uploads, file.FileName);
//    using (var stream = new FileStream(path, FileMode.Create))
//    {
//        await file.CopyToAsync(stream);
//    }



//    return Results.Ok("Upload File");

//}
//).DisableAntiforgery();

app.MapControllers();

app.Run();
