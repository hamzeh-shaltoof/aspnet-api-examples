using ElectronicCommerce.Entities.Base;
using ElectronicCommerce.Enums;

namespace ElectronicCommerce.Entities
{
    public class Payment : BaseEntity
    {
        public decimal TotalAmount { get; set; }
        public PaymentMethod Method { get; set; } 
        public PaymentStatus Status { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;
        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }

}
