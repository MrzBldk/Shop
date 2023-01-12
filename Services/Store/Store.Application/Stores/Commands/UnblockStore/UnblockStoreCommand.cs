using MediatR;
using Store.Application.Common.Exceptions;
using Store.Application.Common.Interfaces;

namespace Store.Application.Stores.Commands.UnblockStore
{
    public record UnblockStoreCommand(Guid Id) : IRequest
    {
    }

    public class UnblockStoreCommandHandler : IRequestHandler<UnblockStoreCommand>
    {
        private readonly IApplicationDbContext _context;

        public UnblockStoreCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UnblockStoreCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.Store entity = await _context.Stores
                .FindAsync(new object[] { request.Id }, cancellationToken);

            if (entity is null)
                throw new NotFoundException(nameof(Domain.Entities.Store), request.Id);

            entity.IsBlocked = false;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
