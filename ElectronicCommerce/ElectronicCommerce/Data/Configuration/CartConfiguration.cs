using ElectronicCommerce.Entities;
using ElectronicCommerce.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ElectronicCommerce.Data.Configuration
{
    public class CartConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.HasOne(x => x.User)
                   .WithMany(x => x.Carts)
                   .HasForeignKey(x => x.UserId)
                   .IsRequired();

            builder.Property(x => x.Status)
                    .HasConversion(x => x.ToString()
                    , x => Enum.Parse<CartStatus>(x))
                    .HasMaxLength(9);

            builder.HasIndex(x => new { x.UserId, x.Status })
                   .HasFilter("[Status] = 'Active'")
                   .IsUnique();

            builder.ToTable("Carts");

            BaseEntityConfiguration.ConfigureBase(builder);
        }
    }
}
