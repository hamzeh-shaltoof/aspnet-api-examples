using ElectronicCommerce.Entities;
using ElectronicCommerce.Entities.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Data;

namespace ElectronicCommerce.Data.Configuration
{
    public static class BaseEntityConfiguration
    {
        public static void ConfigureBase<T>(EntityTypeBuilder<T> builder) where T : BaseEntity
        {


            builder.Property(x => x.Id)
                   .IsRequired()
                   .ValueGeneratedOnAdd();


            builder.Property(x => x.CreatedAt)
                   .HasColumnType("DATETIME")
                   .HasDefaultValueSql("GETDATE()");

            builder.Property(x => x.UpdatedAt)
                   .HasColumnType("DATETIME");


            //  Filter Must Be Placed In The Root Class, Otherwise  Error Will Occur
            //builder.HasQueryFilter(x => x is User && ((User)x).DateDeleted != null);


        }
    } 
}
