namespace HeaderVersioning.Model
{
    public class ProductReview
    {
        public Guid Id { get; set; }

        public int ProductId { get; set; }

        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public int Stars { get; set; }
    }
}
