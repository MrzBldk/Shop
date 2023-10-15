using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Store.Infrasructure.Persistence.Configurations
{
    public class StoreConfiguration : IEntityTypeConfiguration<Domain.Entities.Store>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Store> builder)
        {
            builder.Property(t => t.Name)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(t => t.Description)
                .HasMaxLength(200);

            builder.Property(t => t.UserId)
                .IsRequired();

            builder.Property(t=>t.IsBlocked)
                .IsRequired();
        }
    }
}
