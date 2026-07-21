using ElectronicCommerce.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ElectronicCommerce.Data.Configuration
{
    public class InventoryConfiguration : IEntityTypeConfiguration<Inventory>
    {
        public void Configure(EntityTypeBuilder<Inventory> builder)
        {
            BaseEntityConfiguration.ConfigureBase(builder);

            builder.Property(x => x.Quantity)
                   .IsRequired();

            builder.HasOne(x => x.Warehouse)
                   .WithMany(x => x.Inventories)
                   .HasForeignKey(x => x.WarehouseId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.ProductVariant)
                   .WithMany(x => x.Inventories)
                   .HasForeignKey(x => x.ProductVariantId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => new
            {
                x.ProductVariantId,
                x.WarehouseId,
            }).IsUnique();

            builder.ToTable("Inventories");

                  
    }
    }
}
