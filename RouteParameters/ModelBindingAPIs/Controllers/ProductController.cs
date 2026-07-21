using Microsoft.AspNetCore.Mvc;

namespace ModelBindingAPIs.Controller
{
    [ApiController]
    public class ProductController : ControllerBase 
    {
        [HttpGet("/hamzeh.com-controller-0/{id:int}")]
        public IActionResult TestMethodV0([FromRoute(Name = "id")] int goko)
        {
            return Ok(goko);
        }
        [HttpGet("/hamzeh.com-controller-1/{id:int}")]
        public IActionResult TestMethodV1(int id)
        {
            return Ok(id);
        }
        [HttpGet("/hamzeh.com-controller-2/{id:int}")]
        public IActionResult TestMethodV3([FromQuery(Name ="id")] int goko)
        {
            return Ok(goko);
        }
    }
}
