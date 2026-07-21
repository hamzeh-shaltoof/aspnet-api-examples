using ElectronicCommerce.Entities.Base;

namespace ElectronicCommerce.Entities
{
    public class Comment : BaseEntity
    {
        public int UserId { get; set; } 
        public User User { get; set; } = null!;
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
        public string Content { get; set; } = null!;
    }
}
