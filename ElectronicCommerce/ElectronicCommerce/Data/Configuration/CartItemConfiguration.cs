using ElectronicCommerce.Entities;
using ElectronicCommerce.Entities.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ElectronicCommerce.Data.Configuration
{
    public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            BaseEntityConfiguration.ConfigureBase(builder);

            builder.Property(x => x.Quantity)
                   .IsRequired();


            builder.Property(x => x.UnitPrice)
                   .IsRequired()
                   .HasPrecision(8, 3);

            builder.HasOne(x => x.Cart)
                   .WithMany(x => x.CartItems)
                   .HasForeignKey(x => x.CartId)
                   .IsRequired();

            builder.HasIndex(x => new { x.CartId, x.ProductId })
                   .IsUnique();


            builder.HasOne(x => x.Product)
                   .WithMany()
                   .HasForeignKey(x => x.ProductId)
                   .IsRequired();

            builder.ToTable("CartItems");



        }
    }
}
