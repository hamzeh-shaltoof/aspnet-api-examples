using ElectronicCommerce.Entities;
using ElectronicCommerce.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ElectronicCommerce.Data.Configuration
{
    public class UserCouponConfiguration : IEntityTypeConfiguration<UserCoupon>
    {
        public void Configure(EntityTypeBuilder<UserCoupon> builder)
        {
             BaseEntityConfiguration.ConfigureBase(builder);

            builder.Property(x => x.Status)
             .HasConversion(
                 x => x.ToString(),
                 x => Enum.Parse<CouponStatus>(x))
                  .HasMaxLength(8)
                  .IsUnicode(false)
                  .IsRequired();


            builder.HasOne(x => x.Coupon)
                   .WithMany(x => x.UserCoupons)
                   .HasForeignKey(x => x.CouponId)
                   .OnDelete(DeleteBehavior.Restrict);


            builder.HasOne(x => x.User)
                   .WithMany(x => x.UserCoupons)
                   .HasForeignKey(x => x.UserId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => new { x.UserId, x.CouponId })
                   .IsUnique();

            builder.ToTable("UserCoupons");
        }
    }
}
