using ElectronicCommerce.Entities.Base;

namespace ElectronicCommerce.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; } = null!;
        public bool IsAvailable { get; set; }
        public decimal DiscountRate { get; set; }  // I Could Offer  Discount For Each Specific Major
        public int? ParentId { get; set; }
        public Category? Parent { get; set; } 
        public ICollection<Category>? Children { get; set; } = new List<Category>();
    }

}
