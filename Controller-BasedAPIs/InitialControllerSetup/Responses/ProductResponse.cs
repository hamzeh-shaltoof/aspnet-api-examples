using InitialControllerSetup.Model;

namespace InitialControllerSetup.Responses
{
    public class ProductResponse
    {
        public Guid ProductId { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public List<ProductReviewResponse> Reviews { get; set; } = new List<ProductReviewResponse>();


        private ProductResponse() { }

        public static ProductResponse FromModel(Product product  ,ProductReview productReview = null!)
        {
            if (product is null)
                throw new ArgumentNullException(nameof(product), "This Product Null");

            var response =  new ProductResponse
            {
                ProductId = product.Id,
                Name = product.Name,
                Price = product.Price,
            };

            if (productReview != null)
                ProductReviewResponse.FromModel(productReview);

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
