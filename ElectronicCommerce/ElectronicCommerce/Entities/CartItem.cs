using ElectronicCommerce.Entities.Base;

namespace ElectronicCommerce.Entities
{
    public class CartItem : BaseEntity
    {

        public int CartId { get; set; }
        public Cart Cart { get; set; } = null!;

        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
        
        public short Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        public decimal TotalPrice => Quantity * UnitPrice;

    }
}
