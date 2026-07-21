
using CancellationTokens.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CancellationTokens.Data.Configuration
{
    public class ProductReviewConfiguration : IEntityTypeConfiguration<ProductReview>
    {
        public void Configure(EntityTypeBuilder<ProductReview> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(r => r.Reviewer)
                   .HasMaxLength(100);

            builder.Property(r => r.Stars)
                   .IsRequired();

            builder.HasOne(r => r.Product)
                   .WithMany(p => p.Reviews)
                   .HasForeignKey(r => r.ProductId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasData(
       new ProductReview { Id = Guid.Parse("a0000000-0000-0000-0000-000000000001"), ProductId = Guid.Parse("11111111-1111-1111-1111-111111111111"), Reviewer = "Ahmad", Stars = 5 },
       new ProductReview { Id = Guid.Parse("a0000000-0000-0000-0000-000000000002"), ProductId = Guid.Parse("11111111-1111-1111-1111-111111111111"), Reviewer = "Sara", Stars = 4 },

       new ProductReview { Id = Guid.Parse("a0000000-0000-0000-0000-000000000003"), ProductId = Guid.Parse("22222222-2222-2222-2222-222222222222"), Reviewer = "Ali", Stars = 3 },
       new ProductReview { Id = Guid.Parse("a0000000-0000-0000-0000-000000000004"), ProductId = Guid.Parse("22222222-2222-2222-2222-222222222222"), Reviewer = "Lina", Stars = 5 },

       new ProductReview { Id = Guid.Parse("a0000000-0000-0000-0000-000000000005"), ProductId = Guid.Parse("33333333-3333-3333-3333-333333333333"), Reviewer = "Omar", Stars = 4 },
       new ProductReview { Id = Guid.Parse("a0000000-0000-0000-0000-000000000006"), ProductId = Guid.Parse("33333333-3333-3333-3333-333333333333"), Reviewer = "Noor", Stars = 5 },

       new ProductReview { Id = Guid.Parse("a0000000-0000-0000-0000-000000000007"), ProductId = Guid.Parse("44444444-4444-4444-4444-444444444444"), Reviewer = "Huda", Stars = 4 },
       new ProductReview { Id = Guid.Parse("a0000000-0000-0000-0000-000000000008"), ProductId = Guid.Parse("44444444-4444-4444-4444-444444444444"), Reviewer = "Yousef", Stars = 3 },

       new ProductReview { Id = Guid.Parse("a0000000-0000-0000-0000-000000000009"), ProductId = Guid.Parse("55555555-5555-5555-5555-555555555555"), Reviewer = "Rana", Stars = 5 },
       new ProductReview { Id = Guid.Parse("a0000000-0000-0000-0000-000000000010"), ProductId = Guid.Parse("55555555-5555-5555-5555-555555555555"), Reviewer = "Khaled", Stars = 4 },

       new ProductReview { Id = Guid.Parse("a0000000-0000-0000-0000-000000000011"), ProductId = Guid.Parse("66666666-6666-6666-6666-666666666666"), Reviewer = "Zaid", Stars = 3 },
       new ProductReview { Id = Guid.Parse("a0000000-0000-0000-0000-000000000012"), ProductId = Guid.Parse("66666666-6666-6666-6666-666666666666"), Reviewer = "Maya", Stars = 4 },

       new ProductReview { Id = Guid.Parse("a0000000-0000-0000-0000-000000000013"), ProductId = Guid.Parse("77777777-7777-7777-7777-777777777777"), Reviewer = "Fadi", Stars = 5 },
       new ProductReview { Id = Guid.Parse("a0000000-0000-0000-0000-000000000014"), ProductId = Guid.Parse("77777777-7777-7777-7777-777777777777"), Reviewer = "Dana", Stars = 4 },

       new ProductReview { Id = Guid.Parse("a0000000-0000-0000-0000-000000000015"), ProductId = Guid.Parse("88888888-8888-8888-8888-888888888888"), Reviewer = "Sami", Stars = 3 },
       new ProductReview { Id = Guid.Parse("a0000000-0000-0000-0000-000000000016"), ProductId = Guid.Parse("88888888-8888-8888-8888-888888888888"), Reviewer = "Reem", Stars = 5 },

       new ProductReview { Id = Guid.Parse("a0000000-0000-0000-0000-000000000017"), ProductId = Guid.Parse("99999999-9999-9999-9999-999999999999"), Reviewer = "Tariq", Stars = 4 },
       new ProductReview { Id = Guid.Parse("a0000000-0000-0000-0000-000000000018"), ProductId = Guid.Parse("99999999-9999-9999-9999-999999999999"), Reviewer = "Salma", Stars = 5 },

       new ProductReview { Id = Guid.Parse("a0000000-0000-0000-0000-000000000019"), ProductId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), Reviewer = "Nader", Stars = 4 },
       new ProductReview { Id = Guid.Parse("a0000000-0000-0000-0000-000000000020"), ProductId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), Reviewer = "Farah", Stars = 5 }
   );
        }
    }
}
