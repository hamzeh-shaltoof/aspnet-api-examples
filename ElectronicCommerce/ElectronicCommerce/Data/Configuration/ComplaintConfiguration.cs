using ElectronicCommerce.Entities;
using ElectronicCommerce.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ElectronicCommerce.Data.Configuration
{
    public class ComplaintConfiguration : IEntityTypeConfiguration<Complaint>
    {
        public void Configure(EntityTypeBuilder<Complaint> builder)
        {
            BaseEntityConfiguration.ConfigureBase(builder);

            builder.Property(x => x.Content)
                   .IsRequired()
                   .HasMaxLength(255);

            builder.Property(x => x.Status)
                   .IsRequired()
                   .HasConversion(
                       x => x.ToString(),
                       x => Enum.Parse<ComplainStatus>(x))
                   .HasMaxLength(10)
                   .IsUnicode(false);

            builder.Property(x => x.AdminReply)
                   .IsRequired(false)
                   .HasMaxLength(255);

            builder.HasOne(x => x.User)
                   .WithMany(x => x.Complaints)
                   .HasForeignKey(x => x.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("Complaints");

        }
    }
}
