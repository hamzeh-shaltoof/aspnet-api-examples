using ElectronicCommerce.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ElectronicCommerce.Data.Configuration
{
    public class ProductVariantConfiguration : IEntityTypeConfiguration<ProductVariant>
    {
        public void Configure(EntityTypeBuilder<ProductVariant> builder)
        {

             BaseEntityConfiguration.ConfigureBase(builder);

            builder.HasOne(x => x.Size)
                   .WithMany(x => x.ProductVariants)
                   .HasForeignKey(x => x.SizeId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Product)
                   .WithMany(x => x.ProductVariants)
                   .HasForeignKey(x => x.ProductId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.Quantity)
                   .IsRequired();

            builder.Property(x => x.Price)
                   .IsRequired()
                   .HasPrecision(8, 3);

            builder.Property(x => x.IsActive)
                   .IsRequired()
                   .HasDefaultValue(true);
                  
            builder.Property(x => x.CountryMade)
                   .IsRequired()
                   .IsUnicode(false)
                   .HasMaxLength(20);

            builder.ToTable("ProductVariants");
        }
    }
}
