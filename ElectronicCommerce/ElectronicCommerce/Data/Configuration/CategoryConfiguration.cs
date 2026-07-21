using ElectronicCommerce.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ElectronicCommerce.Data.Configuration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
             BaseEntityConfiguration.ConfigureBase(builder);

            builder.Property(x => x.Name)
                   .IsRequired()
                   .HasMaxLength(40)
                   .HasColumnType("VARCHAR(40)");

            builder.Property(x => x.DiscountRate)
                   .HasPrecision(5, 2)
                   .HasDefaultValue(0.0m);
            builder.Property(x => x.Description)
                   .HasMaxLength(450)
                   .HasColumnType("NVARCHAR(450)");

       
            builder.Property(x => x.IsAvailable)
                   .IsRequired()
                   .HasDefaultValue(true);

            builder.HasOne(x => x.Parent)
                   .WithMany(x => x.Children)
                   .HasForeignKey(x => x.ParentId)
                   .OnDelete(DeleteBehavior.Restrict);

            //builder.HasData(SeedCategories());

            builder.ToTable("Categories");

        }
        //private Category[] SeedCategories()
        //{
        //    return new Category[]
        //    {
        //       new Category { Id = 1, Name = "Electronics", Description = "Gadgets, smartphones, and computers", DiscountRate = 0, IsAvailable = true, CreatedAt = new DateTime(2024, 1, 1) },
        //       new Category { Id = 2, Name = "Books", Description = "Educational and fictional books", DiscountRate = 10.0m, IsAvailable = true, CreatedAt = new DateTime(2024, 1, 1) },
        //       new Category { Id = 3, Name = "Home & Kitchen", Description = "Appliances and furniture", DiscountRate = 5.0m, IsAvailable = true, CreatedAt = new DateTime(2024, 1, 1) }
        //    };
        //}
    }
}
