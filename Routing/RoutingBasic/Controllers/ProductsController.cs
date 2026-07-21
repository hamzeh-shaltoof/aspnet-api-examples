using Microsoft.AspNetCore.Mvc;

namespace RoutingBasic.Controllers
{
    [ApiController]
    [Route("[controller]")] // ../products
    public class ProductsController : ControllerBase
    {
        // ../products/all
        [HttpGet("all")]
        public IActionResult GetProduct()
        {
            return Ok(new []
               {            
                "Product #01",
                "Product #02"
               }
               );
        }
    }
}
