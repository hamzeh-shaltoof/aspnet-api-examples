using ElectronicCommerce.Entities.Base;

namespace ElectronicCommerce.Entities
{
    public class Warehouse : BaseEntity
    {
        public string Name { get; set; } = null!;
        public int AddressId { get; set; }
        public Address Address { get; set; } = null!;
        public ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();
    }

}
