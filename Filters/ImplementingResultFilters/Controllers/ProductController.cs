using Microsoft.AspNetCore.Mvc;

namespace ImplementingResultFilters.Controllers
{
    [Route("/api/products")]
    public class ProductController : ControllerBase 
    {
        [HttpGet]
        public IActionResult GetProduct()
        {
            return Ok(new { Name = "Keyboard", Price = 50m });
        }
    }
}
