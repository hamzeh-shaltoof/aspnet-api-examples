using DataProtection.Data.Configuration;
using DataProtection.Entities;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace DataProtection.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options) , IDataProtectionKeyContext
    {
        public DbSet<Bid> Bids => Set<Bid>();

        public DbSet<DataProtectionKey> DataProtectionKeys => Set<DataProtectionKey>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BidConfiguration).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
