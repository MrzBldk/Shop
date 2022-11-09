using Microsoft.EntityFrameworkCore;

namespace Ordering.Infrastructure.DataAccess
{
    public class Context : DbContext
    {
        public Context(DbContextOptions options) : base(options) { 
            Database.EnsureCreated();
        }

        public DbSet<Entities.Order> Orders { get; set; }
        public DbSet<Entities.OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Entities.OrderItem>()
                .HasOne<Entities.Order>()
                .WithMany()
                .HasForeignKey(i => i.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
}
