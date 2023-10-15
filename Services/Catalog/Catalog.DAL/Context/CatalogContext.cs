using Catalog.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Type = Catalog.DAL.Entities.Type;

namespace Catalog.DAL.Context
{
    public class CatalogContext : DbContext
    {
        public DbSet<Type> Types { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Product> Products { get; set; }

        public CatalogContext(DbContextOptions<CatalogContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ValueConverter<string[], string> splitStringConverter = new(
                v => string.Join(";", v),
                v => v.Split(new[] { ';' }));
            modelBuilder.Entity<Product>().Property(e => e.PicturesUris).HasConversion(splitStringConverter);

            modelBuilder.Entity<Type>().Property(e => e.Created).HasDefaultValueSql("getdate()");
            modelBuilder.Entity<Brand>().Property(e => e.Created).HasDefaultValueSql("getdate()");
            modelBuilder.Entity<Product>().Property(e => e.Created).HasDefaultValueSql("getdate()");

            base.OnModelCreating(modelBuilder);
        }
    }
}
