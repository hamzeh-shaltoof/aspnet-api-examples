using ElectronicCommerce.Entities.Base;

namespace ElectronicCommerce.Entities
{
    public class Search : BaseEntity
    {
        public string Content { get; set; } = null!;

        public int UserId { get; set; }
        public User User { get; set; } = null!;

    }

}
