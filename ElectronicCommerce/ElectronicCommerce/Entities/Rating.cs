using ElectronicCommerce.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace ElectronicCommerce.Entities
{
    public class Rating : BaseEntity
    {
        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
        public byte Stars { get; set; }
    }
}
