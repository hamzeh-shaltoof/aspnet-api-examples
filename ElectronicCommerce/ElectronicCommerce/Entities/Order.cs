using ElectronicCommerce.Entities.Base;
using ElectronicCommerce.Enums;

namespace ElectronicCommerce.Entities
{
    public class Order : BaseEntity
    {
        public decimal Tax { get; set; }
        public decimal TotalPrice { get; set; }
        public OrderStatus Status { get; set; }   
        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public Payment Payment { get; set; } = null!;
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    }
}
