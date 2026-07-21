using ElectronicCommerce.Entities.Base;

namespace ElectronicCommerce.Entities
{
    public class Inventory : BaseEntity
    {
        public int Quantity { get; set; }
        public int ProductVariantId { get; set; }
        public ProductVariant ProductVariant { get; set; } = null!;
        public int WarehouseId { get; set; }
        public Warehouse Warehouse { get; set; } = null!;

    }

}
