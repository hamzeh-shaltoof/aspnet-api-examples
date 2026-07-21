using ElectronicCommerce.Entities.Base;

namespace ElectronicCommerce.Entities
{
    public class UserLoginHistory : BaseEntity
    {
        public int UserId { get; set; }
        public User User { get; set; } = new User();
        public string IpAddress { get; set; } = null!;
        public string UserAgent { get; set; } = null!;
        public bool IsSuccessful { get; set; }
    }
}
