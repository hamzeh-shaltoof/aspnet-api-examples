using ElectronicCommerce.Entities.Base;

namespace ElectronicCommerce.Entities
{
    public class Size : BaseEntity
    {
        public string Name { get; set; } = null!;
        public ICollection<ProductVariant> ProductVariants { get; set; } = new List<ProductVariant>();
    }
}
