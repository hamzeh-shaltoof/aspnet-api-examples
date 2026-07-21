using EFCoreBasics.Data;
using Microsoft.AspNetCore.Mvc;

namespace EFCoreBasics.Controller
{
    [ApiController]
    [Route("api/Head")]
    public class HeadController(ProductRepository repository) : ControllerBase 
    {
        [HttpHead("{productId:guid}")]
        public IActionResult Head(Guid productId)
        {
            return repository.IsExistsById(productId) ? Ok() : NotFound();
        }
    }
}
