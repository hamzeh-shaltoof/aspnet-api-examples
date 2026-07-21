using ElectronicCommerce.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ElectronicCommerce.Data.Configuration
{
    public class UserLoginHistoryConfiguration : IEntityTypeConfiguration<UserLoginHistory>
    {
        public void Configure(EntityTypeBuilder<UserLoginHistory> builder)
        {
        
            builder.Property(x => x.IpAddress)
                   .IsRequired()
                   .IsUnicode(false)
                   .HasMaxLength(45);

            builder.Property(x => x.UserAgent)
                   .IsRequired()
                   .IsUnicode(false)
                   .HasMaxLength(500);

            builder.HasOne(x => x.User)
                   .WithMany(x => x.UserLoginHistories)
                   .HasForeignKey(x => x.UserId)
                   .OnDelete(DeleteBehavior.Restrict);

            BaseEntityConfiguration.ConfigureBase(builder);
            
            builder.ToTable("UserLoginHistories");
        }
    }
}
