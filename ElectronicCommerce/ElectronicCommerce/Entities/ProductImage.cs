using ElectronicCommerce.Entities.Base;

namespace ElectronicCommerce.Entities
{
    public class ProductImage : BaseEntity
    {
        public string Path { get; private set; } = null!;
        public int ProductVariantId { get; set; }
        public ProductVariant ProductVariant { get; set; } = null!;
    }

}
