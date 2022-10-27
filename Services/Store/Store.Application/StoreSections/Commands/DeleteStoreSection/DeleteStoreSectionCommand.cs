using MediatR;
using Store.Application.Common.Exceptions;
using Store.Application.Common.Interfaces;
using Store.Domain.Entities;
using Store.Domain.Events;

namespace Store.Application.StoreSections.Commands.DeleteStoreSection
{
    public record DeleteStoreSectionCommand(Guid Id) : IRequest;

    public class DeleteStoreSectionCommandHandler : IRequestHandler<DeleteStoreSectionCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteStoreSectionCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteStoreSectionCommand request, CancellationToken cancellationToken)
        {
            StoreSection entity = await _context.Sections.FindAsync(new object[] { request.Id }, cancellationToken);

            if (entity is null)
                throw new NotFoundException(nameof(StoreSection), request.Id);

            _context.Sections.Remove(entity);

            entity.AddDomainEvent(new SectionDeletedEvent(entity));

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }

    }
}
