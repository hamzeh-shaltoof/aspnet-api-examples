using Microsoft.AspNetCore.Mvc;
using MediaTypeVersioning.Data;
using MediaTypeVersioning.Responses.V2;

namespace MediaTypeVersioning.Controller.V2
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/products")]
    public class ProductController(ProductRepository repository) : ControllerBase
    {
        [HttpGet("{id:int}")]
        public ActionResult<ProductResponse> Get(int id, [FromQuery] bool isInclude = false)
        {
            var product = repository.GetProductById(id);
            ProductResponse response;

            if (isInclude)
            {
                var review = repository.GetReviewsByProductId(id);
                response = ProductResponse.FromModel(product!, review!);
                return response;
            }
            response = ProductResponse.FromModel(product!);

            return response;

        }
    }
}
