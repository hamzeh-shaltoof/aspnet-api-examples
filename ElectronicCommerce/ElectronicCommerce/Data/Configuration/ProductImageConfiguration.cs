using ElectronicCommerce.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ElectronicCommerce.Data.Configuration
{
    public class ProductImageConfiguration : IEntityTypeConfiguration<ProductImage>
    {
        public void Configure(EntityTypeBuilder<ProductImage> builder)
        {
            BaseEntityConfiguration.ConfigureBase(builder);

            builder.Property(x => x.Path)
                .IsRequired()
                .HasMaxLength(255);

            builder.HasOne(x => x.ProductVariant)
                   .WithMany(x => x.ProductImages)
                   .HasForeignKey(x => x.ProductVariantId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("ProductImages");
        }
    }
}
