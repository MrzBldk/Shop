using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Domain.Entities;

namespace Store.Infrasructure.Persistence.Configurations
{
    public class StoreSectionConfiguration : IEntityTypeConfiguration<StoreSection>
    {
        public void Configure(EntityTypeBuilder<StoreSection> builder)
        {
            builder.Property(t => t.Name).
                HasMaxLength(50).
                IsRequired();

            builder.Property(t => t.Description)
                .HasMaxLength(200);
        }
    }
}
