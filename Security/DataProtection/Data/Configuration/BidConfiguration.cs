using DataProtection.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataProtection.Data.Configuration
{
    public class BidConfiguration : IEntityTypeConfiguration<Bid>
    {
        public void Configure(EntityTypeBuilder<Bid> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Amount)
                .HasColumnType("decimal(18,2)") 
                .IsRequired();

            builder.Property(b => b.BidDate)
                .IsRequired();

            builder.Property(b => b.FirstName)
                .HasMaxLength(50)
                .IsRequired(false); 

            builder.Property(b => b.LastName)
                .HasMaxLength(50)
                .IsRequired(false);

            builder.Property(b => b.Email)
                .HasMaxLength(100)
                .IsRequired(false);

            builder.Property(b => b.Telephone)
                .HasMaxLength(20)
                .IsRequired(false);

            builder.Property(b => b.Address)
                .HasMaxLength(250)
                .IsRequired(false);
        }
    }
}
