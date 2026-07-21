using ElectronicCommerce.Entities.Base;

namespace ElectronicCommerce.Entities
{
    public class Role : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; } = null!;
        public bool IsAvailable { get; set; }

    }
}
