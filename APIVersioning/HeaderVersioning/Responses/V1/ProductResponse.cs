using QueryStringVersioning.Model;

namespace QueryStringVersioning.Responses.V1
{
    public class ProductResponse
    {
        public int ProductId { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public IEnumerable<ProductReviewResponse>? Reviews { get; set; }



        private ProductResponse() { }

        public static ProductResponse FromModel(Product product  , IEnumerable<ProductReview> productReviews = null!)
        {
            if (product is null)
                throw new ArgumentNullException(nameof(product), "This Product Null");
        
            var response =  new ProductResponse
            {
                ProductId = product.Id,
                Name = product.Name,
                Price = product.UnitPrice,
            };

            if (productReviews != null )
                response.Reviews = ProductReviewResponse.FromModel(productReviews);
      

            return response;
        }

        public static IEnumerable<ProductResponse> FromModel(IEnumerable<Product> products)
        {
            if (products is null)
                throw new ArgumentNullException(nameof(products), "This Product Null");

            return products.Select(x => FromModel(x));
        }
    }
}
