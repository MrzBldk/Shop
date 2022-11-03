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
    }
}
