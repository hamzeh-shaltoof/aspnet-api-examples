using EFCoreBasics.Data;
using EFCoreBasics.Request;
using EFCoreBasics.Responses;
using Microsoft.AspNetCore.Mvc;

namespace EFCoreBasics.Controller
{
    [ApiController]
    [Route("api/Put")]
    public class PutController(ProductRepository repository) : ControllerBase
    {
        [HttpPut("{productId:guid}")]
        public IActionResult Put(Guid productId , UpdateProductRequest request )
        {
           var product = repository.GetProductById(productId);
            if (product is null)
                return NotFound("I'm Sorry , Not Found This Product ");

            product.Name = request.Name;
            product.Price = request.Price;
          var successed =  repository.UpdateProduct(product);

            if (!successed)
                return StatusCode(500, "Server Error");
            return CreatedAtRoute(
                                    routeName: "GetProductById",
                                    routeValues: new { productId = product.Id},
                                    value : ProductResponse.FromModel(product));

        }
    }
}
