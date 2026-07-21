using MethodVerb.Data;
using MethodVerb.Model;
using MethodVerb.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace MethodVerb.Controller
{
    [ApiController]
    [Route("api/Get")]
    public class GetController(ProductRepository repository) : ControllerBase 
    {
        [HttpGet("{productId:guid}" , Name = "GetProductById")]
        public ActionResult<ProductResponse> GetProductById(Guid productId , [FromQuery] bool isIncludedReviews = false)
        {
            Product product;
            if (!isIncludedReviews)
            {
                product = repository.GetProductById(productId)!;
                return ProductResponse.FromModel(product);
            }
                 product = repository.GetProductById(productId)!;
                List<ProductReview> reviews = repository.GetReviewsByProductId(productId);

                return ProductResponse.FromModel(product, reviews);
        }

        [HttpGet]
        public ActionResult<PaginationResponse<ProductResponse>> GetPage([FromQuery] int page, [FromQuery] int pageSize , [FromQuery] bool isIncludedReviews = false)
        {
            page = Math.Max(1, page);
            pageSize = Math.Clamp(pageSize, 1, 100);

           var products = repository.GetProductPage(page,pageSize);
           var CountProducts = repository.CountProducts;

            IEnumerable<ProductResponse> productsResponse;
            PaginationResponse<ProductResponse> paginationResponse;
            if (isIncludedReviews)
               { 
                var reviews = repository.ProductReviews;

                 productsResponse = ProductResponse.FromModel(products, reviews);

                var CountReviews = repository.CountProductsReview(products);

                 paginationResponse = PaginationResponse<ProductResponse>
                                         .Create(productsResponse, pageSize, CountProducts, page);
                return paginationResponse;
            }

            productsResponse = ProductResponse.FromModel(products);

            paginationResponse = PaginationResponse<ProductResponse>
                               .Create(productsResponse, pageSize, CountProducts, page);

            return Ok(paginationResponse);
        }
    }
}
