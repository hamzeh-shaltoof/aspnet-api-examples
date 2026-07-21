using ElectronicCommerce.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ElectronicCommerce.Data.Configuration
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            //builder.HasData(SeedRoles());
             BaseEntityConfiguration.ConfigureBase(builder);

            builder.Property(x => x.Description)
                   .HasMaxLength(450)
                   .HasColumnType("NVARCHAR(450)")
                   .HasColumnOrder(14);

            builder.Property(x => x.Name)
                  .IsRequired()
                  .HasMaxLength(40)
                  .HasColumnType("VARCHAR(40)")
                  .HasColumnOrder(2);

            builder.Property(x => x.IsAvailable)
                   .IsRequired()
                   .HasDefaultValue(true);

            builder.ToTable("Roles");

        }
        //private Role[] SeedRoles()
        //{
        //    return new Role[]
        //    {
        //        new Role { Id = 1, Name = "Admin", Description = "Full access to all system modules", IsAvailable = true, CreatedAt = new DateTime(2024, 1, 1) },
        //        new Role { Id = 2, Name = "Editor", Description = "Can manage products and categories", IsAvailable = true, CreatedAt = new DateTime(2024, 1, 1) },
        //        new Role { Id = 3, Name = "Customer", Description = "Can view and purchase products", IsAvailable = true, CreatedAt = new DateTime(2024, 1, 1) }
        //    };
        //}
    }
}
