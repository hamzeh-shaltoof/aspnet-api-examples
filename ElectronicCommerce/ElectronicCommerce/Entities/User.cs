using ElectronicCommerce.Entities.Base;
using ElectronicCommerce.Entities.Contact;
using ElectronicCommerce.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace ElectronicCommerce.Entities
{


    public class User : BaseEntity, ISoftDelete
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateOnly? BirthDate { get; set; }

        [EmailAddress(ErrorMessage = "Invalid Email Format")]
        public string Email { get; set; } = null!;

        [MinLength(8, ErrorMessage = "Must Should be At Least 8 Symbols")]
        public string Password { get; set; } = null!;
        public string? PhoneNumber { get; set; } = null!;
        public string? ProfilePictureUrl { get; set; } = null!;
        public DateTime? LastLoginDate { get; set; }
        public Role Role { get; set; } = null!;
        public int RoleId { get; set; }
        public UserStatus Status { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DateDeleted { get; set; }
        public ICollection<Order> Orders { get; set; } = new List<Order>();
        public ICollection<Address> Addresses { get; set; } = new List<Address>();
        public ICollection<Cart> Carts { get; set; } = new List<Cart>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public ICollection<Rating> Ratings { get; set; } = new List<Rating>();
        public ICollection<UserCoupon> UserCoupons { get; set; } = new List<UserCoupon>();
        public ICollection<Wishlist> Wishlists { get; set; } = new List<Wishlist>();
        public ICollection<Complaint> Complaints { get; set; } = new List<Complaint>();
        public ICollection<Search> Searches { get; set; } = new List<Search>();
        public ICollection<AuditLog> AuditLogs { get; set; } = new List<AuditLog>();
        public ICollection<UserLoginHistory> UserLoginHistories { get; set; } = new List<UserLoginHistory>();
        public string FullName => $"{FirstName} {LastName}";


    }
}
