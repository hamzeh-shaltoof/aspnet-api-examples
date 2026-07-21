using ElectronicCommerce.Entities.Base;
using ElectronicCommerce.Enums;

namespace ElectronicCommerce.Entities
{
    public class AuditLog : BaseEntity
    {
        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public AuditAction Action { get; set; }
        public EntityType EntityType { get; set; }
        public int? EntityId { get; set; }
        public string? OldValues { get; set; }
        public string? NewValues { get; set; }
    }

}
