using EFCoreBasics.Data;
using EFCoreBasics.Model;
using EFCoreBasics.Request;
using EFCoreBasics.Responses;
using Microsoft.AspNetCore.Mvc;

namespace EFCoreBasics.Controller
{
    [ApiController]
    [Route("api/Post")]
    public class PostController(ProductRepository repository) : ControllerBase 
    {
        [HttpPost]
        public IActionResult Post(CreateProductRequest request)
        {
            if (!repository.ExistsByName(request.Name))
            {
                var product =
                           new Model.Product
                           {
                               Id = Guid.NewGuid(),
                               Name = request.Name,
                               Price = request.Price
                           };

                repository.AddProduct(product);

                return CreatedAtRoute(routeName: "GetProductById"
                                    , routeValues: new { productId = product.Id }
                                    , value: ProductResponse.FromModel(product));
            }
            return Conflict($"A Product With Name {request.Name} Already Exists.");

           

       }
    }
}
