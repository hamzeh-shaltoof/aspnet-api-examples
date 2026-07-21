using ElectronicCommerce.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ElectronicCommerce.Data.Configuration
{
    public class CouponConfiguration : IEntityTypeConfiguration<Coupon>
    {
        public void Configure(EntityTypeBuilder<Coupon> builder)
        {
             BaseEntityConfiguration.ConfigureBase(builder);

            builder.Property(x => x.Name)
                   .IsRequired()
                   .IsUnicode(false)
                   .HasMaxLength(20);

            builder.Property(x => x.Code)
                   .IsRequired()
                   .IsUnicode(false)
                   .HasMaxLength(30);

            builder.HasIndex(x => x.Code)
                   .IsUnique();

            builder.Property(x => x.Description)
                  .IsRequired(false)
                  .IsUnicode(false)
                  .HasMaxLength(100);


            builder.ToTable("Coupons");
        }
    }
}
