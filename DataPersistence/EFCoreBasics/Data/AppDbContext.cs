using EFCoreBasics.Model;
using Microsoft.EntityFrameworkCore;

namespace EFCoreBasics
    .Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<ProductReview> ProductReviews { get; set; }
        public DbSet<Product> Products { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
            
    }
}
