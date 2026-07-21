using Microsoft.AspNetCore.Mvc;

namespace Forms.Controllers
{
    [ApiController]
    public class FormController : ControllerBase
    {
        [HttpGet("/product")]
        public IActionResult Get([FromForm] string name , [FromForm] decimal price)
        {
            return Ok($"Product = {name}\nPrice = {price}");
        }

        [HttpPost("upload-controller")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file is null || file.Length == 0) 
                return BadRequest("Not Found This File");

            var uploads = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
             Directory.CreateDirectory(uploads);

            var path = Path.Combine(uploads,file.FileName);
            using (var stream = new FileStream(path, FileMode.Create))
                await file.CopyToAsync(stream);

            return Ok(new
            {
                Message = "File saved successfully!",
                file.FileName,
                file.Length,
                SavedPath = path
            });
        }
    }


}
