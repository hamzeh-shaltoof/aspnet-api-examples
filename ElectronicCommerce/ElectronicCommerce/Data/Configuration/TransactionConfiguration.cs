using ElectronicCommerce.Entities;
using ElectronicCommerce.Helper;
using Microsoft.EntityFrameworkCore;
using ElectronicCommerce.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ElectronicCommerce.Data.Configuration
{
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
             BaseEntityConfiguration.ConfigureBase(builder);

            builder.Property(x => x.TransactionCode)
                   .IsRequired()
                   .HasColumnType("VARCHAR(10)")
                   .HasMaxLength(10);

            builder.Property(x => x.ProviderTransactionId) // From Bank But This Is Fake Data
                   .IsRequired(false)
                   .HasColumnType("VARCHAR(14)")
                   .HasMaxLength(14);

            builder.Property(x => x.Amount)
                   .IsRequired()
                   .HasPrecision(10, 3);

            builder.Property(x => x.Fee)
                   .IsRequired(false)
                   .HasPrecision(5,2);

            builder.Property(x => x.NetAmount)
                   .IsRequired(false)
                   .HasPrecision(10, 3);

            builder.Property(x => x.Status)

                   .HasConversion(
                       x => x.ToString(),
                       x => Enum.Parse<TransactionStatus>(x))
                   .HasMaxLength(8)
                   .IsUnicode(false)
                   .IsRequired();

            builder.Property(x => x.Type)
                   .HasConversion(
                       x => x.ToString(),
                       x => Enum.Parse<TransactionType>(x))
                   .HasMaxLength(17)
                   .IsUnicode(false)
                   .IsRequired();


            builder.Property(x => x.Method)
                   .HasConversion(
                       x => x.ToString(),
                       x => Enum.Parse<PaymentMethod>(x))
                   .HasMaxLength(13)
                   .IsUnicode(false)
                   .IsRequired();

            builder.Property(x => x.Currency)
                   .IsRequired()
                   .HasMaxLength(3)
                   .IsUnicode(false)
                   .HasDefaultValue("JOD");



            builder.HasOne(x => x.Payment)
                   .WithMany(x => x.Transactions)
                   .HasForeignKey(x => x.PaymentId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable("Transactions");

        }
 
    }

}
