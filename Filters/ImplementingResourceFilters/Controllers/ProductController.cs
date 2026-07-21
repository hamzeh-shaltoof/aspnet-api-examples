using ImplementingResourceFilters.Filters;
using Microsoft.AspNetCore.Mvc;

namespace ImplementingResourceFilters.Controllers
{
    [Route("/api/products")]
    [TypeFilter(typeof(TenantValidationFilterV2))]
    public class ProductController : ControllerBase 
    {
        [HttpGet]
        public IActionResult GetProduct()
        {
            return Ok(new { Name = "Keyboard", Price = 50m });
        }
    }
}
