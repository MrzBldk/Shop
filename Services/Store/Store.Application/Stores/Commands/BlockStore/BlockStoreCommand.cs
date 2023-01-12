using MediatR;
using Store.Application.Common.Exceptions;
using Store.Application.Common.Interfaces;

namespace Store.Application.Stores.Commands.BlockStore
{
    public record BlockStoreCommand(Guid Id) : IRequest
    {
    }

    public class BlockStoreCommandHandler : IRequestHandler<BlockStoreCommand>
    {
        private readonly IApplicationDbContext _context;

        public BlockStoreCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(BlockStoreCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.Store entity = await _context.Stores
                .FindAsync(new object[] { request.Id }, cancellationToken);

            if (entity is null)
                throw new NotFoundException(nameof(Domain.Entities.Store), request.Id);

            entity.IsBlocked = true;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
