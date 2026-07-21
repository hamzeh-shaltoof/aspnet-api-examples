using ElectronicCommerce.Entities;
using ElectronicCommerce.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ElectronicCommerce.Data.Configuration
{
    public class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
    {
        public void Configure(EntityTypeBuilder<AuditLog> builder)
        {

            builder.Property(x => x.OldValues)
                   .HasColumnType("VARCHAR(max)");

            builder.Property(x => x.NewValues)
                   .HasColumnType("VARCHAR(max)");

            builder.Property(x => x.Action)
                   .IsRequired()
                   .HasConversion(x => x.ToString(), x => Enum.Parse<AuditAction>(x))
                   .HasMaxLength(6);

            builder.Property(x => x.EntityType)
                  .IsRequired()
                  .HasConversion(x => x.ToString(), x => Enum.Parse<EntityType>(x))
                  .HasMaxLength(7);

            builder.Property(x => x.EntityId)
                   .IsRequired();

            builder.HasOne(x => x.User)
                   .WithMany(x => x.AuditLogs)
                   .HasForeignKey(x => x.UserId)
                   .OnDelete(DeleteBehavior.Restrict);

            BaseEntityConfiguration.ConfigureBase(builder);

            builder.ToTable("AuditLogs");

            
        }
    }
}
