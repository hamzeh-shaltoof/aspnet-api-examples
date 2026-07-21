using ElectronicCommerce.Entities;
using ElectronicCommerce.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ElectronicCommerce.Data.Configuration
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public  void Configure(EntityTypeBuilder<Order> builder)
        {
            BaseEntityConfiguration.ConfigureBase(builder);


            builder.Property(x => x.Tax)
                   .IsRequired()
                   .HasPrecision(5, 2)
                   .HasDefaultValue(0.0m);

            builder.Property(x => x.TotalPrice)
                   .IsRequired()
                   .HasPrecision(10, 3);

            builder.Property(x => x.Status)
                   .HasConversion(
                x => x.ToString(),
                x => Enum.Parse<OrderStatus>(x))
                   .HasColumnType("VARCHAR(10)")
                   .HasDefaultValue(OrderStatus.Pending);

            builder.HasOne(x => x.User)
                   .WithMany(x => x.Orders)
                   .HasForeignKey(x => x.UserId);

            builder.ToTable("Orders");


        }

    }
}
