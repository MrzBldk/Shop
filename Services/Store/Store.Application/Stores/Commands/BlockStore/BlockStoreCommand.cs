using MediatR;
using Store.Application.Common.Exceptions;
using Store.Application.Common.Interfaces;
using Store.Application.IntegrationEvents;
using Store.Application.IntegrationEvents.Events;

namespace Store.Application.Stores.Commands.BlockStore
{
    public record BlockStoreCommand(Guid Id) : IRequest
    {
    }

    public class BlockStoreCommandHandler : IRequestHandler<BlockStoreCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly IStoreIntegrationService _storeIntegrationService;

        public BlockStoreCommandHandler(IApplicationDbContext context, IStoreIntegrationService storeIntegrationService)
        {
            _context = context;
            _storeIntegrationService = storeIntegrationService;
        }

        public async Task<Unit> Handle(BlockStoreCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.Store entity = await _context.Stores
                .FindAsync(new object[] { request.Id }, cancellationToken);

            if (entity is null)
                throw new NotFoundException(nameof(Domain.Entities.Store), request.Id);

            entity.IsBlocked = true;

            StoreBlockedIntegrationEvent storeBlockedEvent = new(request.Id);
            await _storeIntegrationService.SaveEventAndStoreContextChangesAsync(storeBlockedEvent);
            await _storeIntegrationService.PublishThrougEcentBusAsync(storeBlockedEvent);

            return Unit.Value;
        }
    }
}
