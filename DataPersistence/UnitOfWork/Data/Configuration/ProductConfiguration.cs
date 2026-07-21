using UnitOfWork.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace UnitOfWork.Data.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(p => p.Price)
                   .HasColumnType("decimal(10,2)")
                   .IsRequired();

            builder.HasData(
          new Product { Id = Guid.Parse("11111111-1111-1111-1111-111111111111"), Name = "Laptop", Price = 750.00m },
          new Product { Id = Guid.Parse("22222222-2222-2222-2222-222222222222"), Name = "Mouse", Price = 25.50m },
          new Product { Id = Guid.Parse("33333333-3333-3333-3333-333333333333"), Name = "Keyboard", Price = 45.00m },
          new Product { Id = Guid.Parse("44444444-4444-4444-4444-444444444444"), Name = "Monitor", Price = 180.00m },
          new Product { Id = Guid.Parse("55555555-5555-5555-5555-555555555555"), Name = "Headphones", Price = 60.00m },
          new Product { Id = Guid.Parse("66666666-6666-6666-6666-666666666666"), Name = "Webcam", Price = 70.00m },
          new Product { Id = Guid.Parse("77777777-7777-7777-7777-777777777777"), Name = "Microphone", Price = 95.00m },
          new Product { Id = Guid.Parse("88888888-8888-8888-8888-888888888888"), Name = "Printer", Price = 150.00m },
          new Product { Id = Guid.Parse("99999999-9999-9999-9999-999999999999"), Name = "Tablet", Price = 300.00m },
          new Product { Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), Name = "Smartphone", Price = 650.00m },
          new Product { Id = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), Name = "Smartphone", Price = 600.00m },
          new Product { Id = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"), Name = "Smartphone", Price = 550.00m }
      );
        }
    }
}
