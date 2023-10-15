using Microsoft.EntityFrameworkCore;
using Store.Domain.Entities;

namespace Store.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Domain.Entities.Store> Stores { get; }
        DbSet<StoreSection> Sections { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
