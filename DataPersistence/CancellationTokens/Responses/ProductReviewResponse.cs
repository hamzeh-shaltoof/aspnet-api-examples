using CancellationTokens.Model;

namespace CancellationTokens.Responses
{
    public class ProductReviewResponse
    {
        public Guid ReviewId { get; set; }

        public Guid ProductId { get; set; }

        public string? Reviewer { get; set; }

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
                Reviewer = productReview.Reviewer,
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
