using EFCoreBasics.Data;
using Microsoft.AspNetCore.Mvc;

namespace EFCoreBasics.Controller
{
    [ApiController]
    [Route("api/delete")]
    public class DeleteController(ProductRepository repository) : ControllerBase
    {
        [HttpDelete("{productId:guid}")]
        public IActionResult Delete(Guid productId)
        {
           var isFound = repository.IsExistsById(productId);
         
            if(!isFound)
                return NotFound($"Not Found The Product With");

            var succeed = repository.DeleteProduct(productId);
            if (!succeed)
                return StatusCode(500, "Error Server");

            return NoContent();
        }
    }
}
