using ElectronicCommerce.Entities;
using ElectronicCommerce.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ElectronicCommerce.Data.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {

             BaseEntityConfiguration.ConfigureBase(builder);
            builder.Property(x => x.FirstName)
                   .IsRequired()
                   .HasColumnType("VARCHAR(20)")
                   .ValueGeneratedOnAdd();

            builder.Property(x => x.LastName)
                   .IsRequired()
                   .HasColumnType("VARCHAR(20)");

            builder.Property(x => x.BirthDate)
                   .HasColumnType("DATE");

            builder.Property(x => x.Email)
                   .IsRequired()
                   .HasColumnType("VARCHAR(30)");

            builder.Property(x => x.Password)
                    .IsRequired()
                    .HasColumnType("VARCHAR(50)");

            builder.Property(x => x.PhoneNumber)
                    .HasColumnType("VARCHAR(15)");

            builder.Property(x => x.ProfilePictureUrl)
                    .HasColumnType("NVARCHAR(500)");

            builder.Property(x => x.LastLoginDate)
                    .HasColumnType("DATETIME");

            builder.Property(x => x.RoleId)
                   .IsRequired();

            builder.HasIndex(x => x.Email)
                   .IsUnique()
                   .HasFilter("[DateDeleted] IS NULL");

            builder.Property(x => x.Status)
                   .HasConversion(
                       x => x.ToString(),
                       x => Enum.Parse<UserStatus>(x))
                   .HasColumnType("VARCHAR(9)")
                   .HasDefaultValue(UserStatus.Active);

            builder.HasQueryFilter(x => x.IsDeleted == false);


            builder.ToTable("Users");

            //builder.HasData(SeedUsers());

        }

        //private User[] SeedUsers()
        //{
        //    return new User[]
        //    {
        //      new User {Id = 1,FirstName = "hamzeh", LastName = "marwan", Email = "hamzeh.marwan123@gmail.com" , Password = "12345678" , RoleId = 1 },
        //      new User {Id = 2,FirstName = "mosa", LastName = "khalid", Email = "mosa.khalid123@gmail.com" , Password = "12345678" , RoleId = 2 },
        //      new User {Id = 3,FirstName = "tamer", LastName = "rageb", Email = "tamer.rageb123@gmail.com" , Password = "12345678" , RoleId = 3 }
        //    };
        //}
    }
}
