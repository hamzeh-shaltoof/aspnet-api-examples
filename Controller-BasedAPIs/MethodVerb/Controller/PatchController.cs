using MethodVerb.Data;
using MethodVerb.Request;
using MethodVerb.Responses;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace MethodVerb.Controller
{
    [ApiController]
    [Route("api/patch")]
    public class PatchController(ProductRepository repository) : ControllerBase 
    {
        [HttpPatch("{productId:guid}")]
        public IActionResult Patch(Guid ProductId , [FromBody] JsonPatchDocument<UpdateProductRequest> requestDoc)
        {
            if (requestDoc is null)
              return  BadRequest("The Request Is Null");
            var product = repository.GetProductById(ProductId);

            if( product == null)
                return NotFound($"Not Found The Product ");

            var updateModel = new UpdateProductRequest { Price = product.Price };

            requestDoc.ApplyTo(updateModel);

            product.Price = updateModel.Price;

            var succeed = repository.UpdateProduct(product);
            if (!succeed)
                return StatusCode(500, "Server Error");

            return CreatedAtRoute(routeName: "GetProductById",
                                  routeValues: new { productId = product.Id },
                                  ProductResponse.FromModel(product));

        }
    }
}
