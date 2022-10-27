using MediatR;
using Store.Application.Common.Interfaces;
using Store.Domain.Entities;
using Store.Domain.Enums;
using Store.Domain.Events;

namespace Store.Application.StoreSections.Commands.CreateStoreSection
{
    public record CreateStoreSectionCommand : IRequest<Guid>
    {
        public Guid StoreId { get; init; }
        public string Name { get; init; }
        public string? Description { get; init; }
        public PriorityLevel Priority { get; init; }
    }

    public class CreateStoreCommandHanler : IRequestHandler<CreateStoreSectionCommand, Guid>
    {
        private readonly IApplicationDbContext _context;

        public CreateStoreCommandHanler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateStoreSectionCommand request, CancellationToken cancellationToken)
        {
            StoreSection entity = new()
            {
                Name = request.Name,
                Description = request.Description,
                StoreId = request.StoreId,
                Priority = request.Priority
            };

            entity.AddDomainEvent(new SectionCreatedEvent(entity));

            _context.Sections.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
