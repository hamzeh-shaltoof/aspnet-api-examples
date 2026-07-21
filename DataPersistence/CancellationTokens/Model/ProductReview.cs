namespace CancellationTokens.Model
{
    public class ProductReview
    {
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }
        public Product Product { get; set; } = null!;

        public string? Reviewer { get; set; }

        public int Stars { get; set; }
    }
}
