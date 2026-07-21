using ElectronicCommerce.Entities.Base;
using ElectronicCommerce.Enums;
using System.Collections.ObjectModel;

namespace ElectronicCommerce.Entities
{
    public class Cart : BaseEntity
    {
        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public CartStatus Status { get; set; }
        public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
    }
}
