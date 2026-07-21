using ElectronicCommerce.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ElectronicCommerce.Data.Configuration
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.Property(x => x.Country)
                   .HasMaxLength(30);

            builder.Property(x => x.City)
                   .HasMaxLength(30);

            builder.Property(x => x.Street)
                   .HasMaxLength(30);

            builder.Property(x => x.BuildingNumber)
                   .HasMaxLength(20);

            builder.Property(x => x.ZipCode)
                   .HasMaxLength(18);

            builder.HasOne(x => x.User)
                   .WithMany(x => x.Addresses)
                   .HasForeignKey(x => x.UserId)
                   .OnDelete(DeleteBehavior.Cascade);
                   //.IsRequired(false);

            BaseEntityConfiguration.ConfigureBase(builder);

            builder.ToTable("Addresses");


        }
    }
}
