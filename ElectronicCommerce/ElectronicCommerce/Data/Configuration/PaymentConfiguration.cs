using ElectronicCommerce.Entities;
using ElectronicCommerce.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ElectronicCommerce.Data.Configuration
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
             BaseEntityConfiguration.ConfigureBase(builder);

            builder.Property(x => x.TotalAmount)
                   .IsRequired()
                   .HasPrecision(10, 3);

            builder.Property(x => x.Status)
                   .HasConversion(
                        x => x.ToString(),
                        x => Enum.Parse<PaymentStatus>(x))
                   .HasMaxLength(17)
                   .IsUnicode(false)
                   .IsRequired();

            builder.Property(x => x.Method)

                   .HasConversion(
                         x => x.ToString(),
                         x => Enum.Parse<PaymentMethod>(x))
                   .HasMaxLength(13)
                   .IsUnicode(false)
                   .IsRequired();

            builder.HasOne(x => x.Order)
                   .WithOne(x => x.Payment)
                   .HasForeignKey<Payment>(x => x.OrderId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Restrict);
                   

            builder.ToTable("Payments");

        }
 
    }

}
