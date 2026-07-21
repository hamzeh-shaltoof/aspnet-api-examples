using ElectronicCommerce.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ElectronicCommerce.Data.Configuration
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            BaseEntityConfiguration.ConfigureBase(builder);

            builder.Property(x => x.Quantity)
                   .IsRequired();

            builder.Property(x => x.UnitPrice)
                   .IsRequired()
                   .HasPrecision(8, 3);


            builder.HasOne(x => x.Product)
                   .WithMany(x => x.OrderItems)
                   .HasForeignKey(x =>  x.ProductId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Order)
                   .WithMany(x => x.OrderItems)
                   .HasForeignKey(x =>  x.OrderId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable("OrderItems");




        }
    }
}
