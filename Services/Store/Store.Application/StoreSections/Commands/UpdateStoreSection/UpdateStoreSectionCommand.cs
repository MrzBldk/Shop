using MediatR;
using Store.Application.Common.Exceptions;
using Store.Application.Common.Interfaces;
using Store.Domain.Entities;
using Store.Domain.Enums;

namespace Store.Application.StoreSections.Commands.UpdateStoreSection
{
    public record UpdateStoreSectionCommand : IRequest
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public string? Description { get; init; }
        public PriorityLevel Priority { get; init; }
    }

    public class UpdateStoreSectionCommandHandler : IRequestHandler<UpdateStoreSectionCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateStoreSectionCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateStoreSectionCommand request, CancellationToken cancellationToken)
        {
            StoreSection entity = await _context.Sections.FindAsync(new object[] { request.Id }, cancellationToken);

            if (entity is null)
                throw new NotFoundException(nameof(StoreSection), request.Id);

            entity.Name = request.Name;
            entity.Description = request.Description;
            entity.Priority = request.Priority;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
