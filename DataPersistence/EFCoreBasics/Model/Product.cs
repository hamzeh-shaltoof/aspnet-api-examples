namespace EFCoreBasics.Model
{
    public class Product
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public ICollection<ProductReview> Reviews { get; set; } = new List<ProductReview>();

    }
}
