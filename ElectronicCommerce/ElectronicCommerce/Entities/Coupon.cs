using ElectronicCommerce.Entities.Base;
using ElectronicCommerce.Helper;

namespace ElectronicCommerce.Entities
{
    public class Coupon : BaseEntity
    {
        public Coupon()
        {
            // Move Code !
            this.Code = CouponHelper.GetCouponCode();
        }
        public string Name { get; set; } = null!;
        public string Code { get; set; } = null!;
        public string? Description { get; set; }
        public ICollection<UserCoupon> UserCoupons { get; set; } = new List<UserCoupon>();
    }
}
