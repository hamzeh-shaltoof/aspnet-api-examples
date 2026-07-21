using Microsoft.AspNetCore.Mvc;

namespace BodyBinding.Controllers
{
    [ApiController]
    public class Controller : ControllerBase
    {
        [HttpPost("productBodyXML-controller")]
        public IActionResult Post(Product product)
        {
                return Ok(product);
        }
    }
}
