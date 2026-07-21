using MinimalAPIQueryStringVersioning.Model;

namespace MinimalAPIQueryStringVersioning.Responses.V2

{
    public class ProductReviewResponse
    {
        public Guid ReviewId { get; set; }

        public int ProductId { get; set; }

        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public int Stars { get; set; }

        private ProductReviewResponse() { }

        public static ProductReviewResponse FromModel(ProductReview productReview)
        {
            if(productReview is null)
                throw new ArgumentNullException(nameof(productReview) , "This Is Null");

            return new ProductReviewResponse
            {
                ReviewId = productReview.Id,
                ProductId = productReview.ProductId,
                FirstName = productReview.FirstName,
                LastName = productReview.LastName,
                Stars = productReview.Stars,
            };
        }
        public static IEnumerable<ProductReviewResponse> FromModel(IEnumerable<ProductReview> productReviews)
        {
            if(productReviews is null)
                throw new ArgumentNullException(nameof(productReviews) , "This Is Null");

            return productReviews.Select(FromModel);
        }
    }


}
