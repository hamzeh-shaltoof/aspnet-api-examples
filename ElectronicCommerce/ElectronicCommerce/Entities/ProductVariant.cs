using ElectronicCommerce.Entities.Base;
using ElectronicCommerce.Entities.Contact;

namespace ElectronicCommerce.Entities
{
    public class ProductVariant : BaseEntity 
    {
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
        public int SizeId { get; set; }
        public Size Size { get; set; } = null!;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string CountryMade { get; set; } = null!;
        public bool IsActive { get; set; }
        public ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();
        public ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();
        //public decimal CalculateDiscount => this.Price - (this.DiscountRate / 100) * this.Price;
    }

}
