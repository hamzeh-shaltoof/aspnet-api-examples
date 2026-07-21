
using ElectronicCommerce.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ElectronicCommerce.Data.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
             BaseEntityConfiguration.ConfigureBase(builder);


            builder.Property(x => x.Name)
                   .IsRequired()
                   .HasMaxLength(40)
                   .HasColumnType("VARCHAR(40)");

            builder.Property(x => x.ImageUrl)
                   .HasMaxLength(250)
                   .HasColumnType("VARCHAR(250)");


            builder.Property(x => x.DiscountRate)
                   .IsRequired()
                   .HasPrecision(5, 2)
                   .HasDefaultValue(0.0m);

            builder.Property(x => x.Description)
                   .HasMaxLength(450)
                   .HasColumnType("NVARCHAR(450)");

            builder.Property(x => x.IsAvailable)
                   .IsRequired()
                   .HasDefaultValue(true);

            builder.HasOne(x => x.Category)
                   .WithMany()
                   .HasForeignKey(x => x.CategoryId)
                   .OnDelete(DeleteBehavior.Restrict);

            //builder.HasData(SeedProducts());

            builder.ToTable("Products");

            builder.HasQueryFilter(x => x.IsDeleted == false);


        }

        //private Product[] SeedProducts()
        //{
        //    return new Product[]
        //    {
        //new Product
        //{
        //    Id = 1, Name = "iPhone 15 Pro", Description = "Latest Apple smartphone", Price = 1200.00m,
        //    Quantity = 10, ImageUrl = "iphone15.jpg", IsAvailable = true, DiscountRate = 0,
        //    CategoryId = 1, CreatedAt = new DateTime(2024, 1, 1)
        //},
        //new Product
        //{
        //    Id = 2, Name = "C# Programming Book", Description = "Mastering EF Core", Price = 45.50m,
        //    Quantity = 50, ImageUrl = "csharp_book.jpg", IsAvailable = true, DiscountRate = 15.0m,
        //    CategoryId = 2, CreatedAt = new DateTime(2024, 1, 1)
        //},
        //new Product
        //{
        //    Id = 3, Name = "Air Fryer", Description = "Healthy cooking appliance", Price = 150.00m,
        //    Quantity = 5, ImageUrl = "airfryer.jpg", IsAvailable = true, DiscountRate = 5.0m,
        //    CategoryId = 3, CreatedAt = new DateTime(2024, 1, 1)
        //}
        //    };
        //}
    }
}
