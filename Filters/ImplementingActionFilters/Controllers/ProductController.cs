using ImplementingActionFilters.Filters;
using Microsoft.AspNetCore.Mvc;

namespace ImplementingActionFilters.Controllers
{
    [Route("/api/products")]
    [TrackActionTimeFilterV2]
    public class ProductController : ControllerBase 
    {
        [HttpGet]
        public IActionResult GetProduct()
        {
            return Ok(new { Name = "Keyboard", Price = 50m });
        }
    }
}
