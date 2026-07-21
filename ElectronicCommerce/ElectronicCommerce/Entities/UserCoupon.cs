using ElectronicCommerce.Entities.Base;
using ElectronicCommerce.Enums;

namespace ElectronicCommerce.Entities
{
    public class UserCoupon : BaseEntity
    {
        public int CouponId { get; set; }
        public Coupon Coupon { get; set; } = null!;

        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public CouponStatus Status { get; set; }
    }
}
