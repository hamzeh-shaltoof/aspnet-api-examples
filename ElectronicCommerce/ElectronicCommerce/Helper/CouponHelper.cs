namespace ElectronicCommerce.Helper
{
    public static class CouponHelper
    {
        public static string GetCouponCode()
        {
            return "CPN-" + Guid.NewGuid().ToString("N").Substring(0,8).ToUpper();
        }
    }
}
