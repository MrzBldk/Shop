using MediatR;
using Microsoft.EntityFrameworkCore;
using Store.Application.Common.Interfaces;
using Store.Domain.Entities;
using Store.Infrasructure.Common;
using Store.Infrasructure.Persistence.Interceptors;
using System.Reflection;

namespace Store.Infrasructure.Persistence
{
    public class StoreDbContext : DbContext, IApplicationDbContext
    {
        private readonly IMediator _mediator;
        private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;

        public StoreDbContext(
            DbContextOptions<StoreDbContext> options,
            IMediator mediator,
            AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor)
            : base(options)
        {
            _mediator = mediator;
            _auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;
            Database.EnsureCreated();
        }

        public DbSet<Domain.Entities.Store> Stores => Set<Domain.Entities.Store>();
        public DbSet<StoreSection> Sections => Set<StoreSection>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(_auditableEntitySaveChangesInterceptor);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _mediator.DispatchDomainEvents(this);

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
