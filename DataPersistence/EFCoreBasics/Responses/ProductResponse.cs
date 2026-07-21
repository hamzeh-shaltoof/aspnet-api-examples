using EFCoreBasics.Data;
using EFCoreBasics.Model;

namespace EFCoreBasics.Responses
{
    public class ProductResponse
    {
        public Guid ProductId { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public  List<ProductReviewResponse> Reviews { get; set; } = null;


        private ProductResponse() { }

        public static ProductResponse FromModel(Product product  ,IEnumerable<ProductReview> productReview = null)
        {
            if (product is null)
                throw new ArgumentNullException(nameof(product), "This Product Null");

            var response = new ProductResponse
            {
                ProductId = product.Id,
                Name = product.Name,
                Price = product.Price,
                Reviews = null
            };

            response.Reviews = productReview != null && productReview.Any()
                                ? ProductReviewResponse.FromModel(productReview).ToList()
                                      : null!;

            return response;
        }

        public static IEnumerable<ProductResponse> FromModel(IEnumerable<Product> products , IEnumerable<ProductReview> productReview = null )
        {
            if (products is null)
                throw new ArgumentNullException(nameof(products), "This Product Null");

            if(productReview is not null)
            {
                var reviews = productReview.ToLookup(x => x.ProductId);

                return products.Select(x => FromModel(x, reviews[x.Id]));
            }
            return products.Select(x => FromModel(x) );
      
        }

    
    }
}
