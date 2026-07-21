using ElectronicCommerce.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ElectronicCommerce.Data.Configuration
{
    public class WarehouseConfiguration : IEntityTypeConfiguration<Warehouse>
    {
        public void Configure(EntityTypeBuilder<Warehouse> builder)
        {
            BaseEntityConfiguration.ConfigureBase(builder);

            builder.Property(x => x.Name)
                   .IsRequired()
                   .IsUnicode(false)
                   .HasMaxLength(30);

            builder.HasOne(x => x.Address)
                   .WithOne(x => x.Warehouse)
                   .HasForeignKey<Warehouse>(x => x.AddressId);

            builder.ToTable("Warehouses");
      
    }
    }
}
