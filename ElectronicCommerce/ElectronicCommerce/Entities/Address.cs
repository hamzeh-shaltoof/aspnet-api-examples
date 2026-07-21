using ElectronicCommerce.Entities.Base;

namespace ElectronicCommerce.Entities
{
    public class Address : BaseEntity
    {
        public string Country { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Street { get; set; } = null!;
        public string BuildingNumber { get; set; } = null!;
        public int? UserId { get; set; }
        public User? User { get; set; } = null!;
        public string? ZipCode { get; set; } = null!;
        public Warehouse? Warehouse { get; set; } = null!;
    }
}
