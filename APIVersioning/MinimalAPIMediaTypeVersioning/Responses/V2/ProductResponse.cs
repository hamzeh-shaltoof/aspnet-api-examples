using MinimalAPIMediaTypeVersioning.Model;

namespace MinimalAPIMediaTypeVersioning.Responses.V2
{
    public class ProductResponse
    {
        public int ProductId { get; set; }
        public string? Name { get; set; }
        public DetailsResponse DetailsResponse { get; set; }
        public IEnumerable<ProductReviewResponse> Reviews { get; set; } = new List<ProductReviewResponse>();


        private ProductResponse() { }

        public static ProductResponse FromModel(Product product  ,IEnumerable<ProductReview> productReviews = null!)
        {
            if (product is null)
                throw new ArgumentNullException(nameof(product), "This Product Null");

            var response =  new ProductResponse
            {
                ProductId = product.Id,
                Name = product.Name,
                DetailsResponse = new DetailsResponse
                {
                    UnitPrice = product.UnitPrice,
                    CurrentPrice = "JO",
                    Quantity = product.Quantity,
                },
                Reviews = null
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
