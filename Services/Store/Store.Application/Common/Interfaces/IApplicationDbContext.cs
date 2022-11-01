using Microsoft.EntityFrameworkCore;
using Store.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Domain.Entities.Store> Stores { get; }
        DbSet<StoreSection> Sections { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
