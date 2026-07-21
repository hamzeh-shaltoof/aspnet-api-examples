using ElectronicCommerce.Data.Interceptor;
using ElectronicCommerce.Entities.Base;
using ElectronicCommerce.Entities.Contact;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ElectronicCommerce.Entities
{
    public class Wishlist : BaseEntity
    {
        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
    }


    public class ShippingCompany
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Phone { get; set; } 
        public bool IsActive { get; set; }
        public ICollection<ShippingMethod> ShippingMethods { get; set; } = new List<ShippingMethod>();
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public class ShippingMethod
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public ShippingCompany ShippingCompany { get; set; } = null!;
        public int ShippingCompanyId { get; set; } 
        public ShippingMethodType Type { get; set; }
        public WeekDays AvailableDays { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

    }
    public enum ShippingMethodType
    {
        Express,
        Standard,
        SameDay
    }

    [Flags]
    public enum WeekDays
    {
        None = 0,
        Saturday = 1,
        Sunday = 2,
        Monday = 4,
        Tuesday = 8,
        Wednesday = 16,
        Thursday = 32,
        Friday = 64
    }

    public class Supplier : BaseEntity, ISoftDelete
    {
        public string Name { get; set; } = null!;
        public SupplierStatus Status { get; set; }
        public string Phone { get; set; } = null!;
        public bool IsDeleted { get; set; }
        public DateTime? DateDeleted { get; set; }
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
    public enum SupplierStatus
    {
        Active,
        Suspended,
        Inactive,
        Blacklisted
    }

    public class Brand : BaseEntity, ISoftDelete
    {
        public string Name { get; set; } = null!;
        public bool IsDeleted { get; set; }
        public DateTime? DateDeleted { get; set; }
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }

    //Entities Down it is Need  very much think and not completed (this is much error) 
    public class UserDiscount
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public bool IsActive { get; set; }
        public decimal Value { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public ICollection<User> Users { get; set; } = new List<User>();
    }
    public class ProductDiscount
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public bool IsActive { get; set; }
        public decimal Value { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ProductDiscountType Type { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public ICollection<User> Users { get; set; } = new List<User>();
    }

    public enum ProductDiscountType
    {
        Category,
        VIP,
    }
    public class UserDiscountUser
    {
        public int Id { get; set; }
        public int DiscountId { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

    }
    public class ProductDiscountProduct
    {
        public int Id { get; set; }
        public int DiscountId { get; set; }
        public int ProductVariantId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
