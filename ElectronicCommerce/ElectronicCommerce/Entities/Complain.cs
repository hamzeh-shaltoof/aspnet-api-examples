using ElectronicCommerce.Entities.Base;
using ElectronicCommerce.Enums;

namespace ElectronicCommerce.Entities
{
    public class Complaint : BaseEntity
    {
        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public string Content { get; set; } = null!;
        public ComplainStatus Status { get; set; } = ComplainStatus.Pending;
        public string? AdminReply { get; set; }

    }

}
