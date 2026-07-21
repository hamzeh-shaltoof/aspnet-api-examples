namespace ElectronicCommerce.DTOs.Product
{
    public class UpdateProductResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public bool IsAvailable { get; set; }
        public decimal DiscountRate { get; set; }
        public string? Description { get; set; } = null!;
    }
}
