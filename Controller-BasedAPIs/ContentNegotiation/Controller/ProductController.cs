using Microsoft.AspNetCore.Mvc;

namespace ContentNegotiation.Controller
{
    [ApiController]
    [Route("api/get")]

    [Produces("application/json")]
    public class ProductController : ControllerBase
    {
        [HttpGet]
        public IActionResult get()
        {
            return Ok(new Product()
            {
                Id = "53sdf5fhs35",
                Name = "Banana",
                Price = 2.33
            });
        }
    }

    public class Product
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
    }
}
