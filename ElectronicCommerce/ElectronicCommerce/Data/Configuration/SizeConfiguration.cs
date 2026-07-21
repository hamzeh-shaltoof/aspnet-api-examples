using ElectronicCommerce.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ElectronicCommerce.Data.Configuration
{
    public class SizeConfiguration : IEntityTypeConfiguration<Size>
    {
        public void Configure(EntityTypeBuilder<Size> builder)
        {
            BaseEntityConfiguration.ConfigureBase(builder);

            builder.Property(x => x.Name)
                   .IsRequired()
                   .IsUnicode(false)
                   .HasMaxLength(30);



            builder.ToTable("Sizes");
        }
    }

}
