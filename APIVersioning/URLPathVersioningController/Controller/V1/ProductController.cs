using Microsoft.AspNetCore.Mvc;
using URLPathVersioningController.Data;
using URLPathVersioningController.Responses.V1;

namespace URLPathVersioningController.Controller.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/products")]
    [Route("api/v{Version:ApiVersion}/products")]
    public class ProductController(ProductRepository repository) : ControllerBase
    {
        [HttpGet("{id:int}")]
        public ActionResult<ProductResponse> Get(int id, [FromQuery] bool isInclude = false)
        {
            Response.Headers["Deprecation"] = "true";
            Response.Headers["Sunset"] = "WED , OCT 7/10/2026  12:00:00";
            Response.Headers["link"] = "<api/v2/products>; rel = \"successor-version\" ";

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
