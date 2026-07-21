using FluentValidatinsUsingController.Requests;
using Microsoft.AspNetCore.Mvc;

namespace DataAnnotationValidation.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        [HttpPost("create")]
        public IActionResult CreateProduct([FromBody] CreateProductRequest request) 
        {
            return Created($"api/products/{Guid.NewGuid()}", request);
        }
    }
}
