using ElectronicCommerce.Entities.Base;
using ElectronicCommerce.Entities.Contact;

namespace ElectronicCommerce.Entities
{
    public class Product : BaseEntity, ISoftDelete
    {
        public string Name { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public bool IsAvailable { get; set; }
        public decimal DiscountRate { get; set; }
        public string? Description { get; set; } = null!;
        public bool IsDeleted { get; set; }
        public DateTime? DateDeleted { get; set; }
        public Category Category { get; set; } = null!;
        public int CategoryId { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public ICollection<Rating> Ratings { get; set; } = new List<Rating>();
        public ICollection<ProductVariant> ProductVariants { get; set; } = new List<ProductVariant>();
        public ICollection<Wishlist> Wishlists { get; set; } = new List<Wishlist>();

        //public decimal CalculateDiscount => this.Price - (this.DiscountRate / 100) * this.Price;

    }
}
