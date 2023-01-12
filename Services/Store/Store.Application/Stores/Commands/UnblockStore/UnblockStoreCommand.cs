using MediatR;
using Store.Application.Common.Exceptions;
using Store.Application.Common.Interfaces;
using Store.Application.IntegrationEvents.Events;
using Store.Application.IntegrationEvents;

namespace Store.Application.Stores.Commands.UnblockStore
{
    public record UnblockStoreCommand(Guid Id) : IRequest
    {
    }

    public class UnblockStoreCommandHandler : IRequestHandler<UnblockStoreCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly IStoreIntegrationService _storeIntegrationService;

        public UnblockStoreCommandHandler(IApplicationDbContext context, IStoreIntegrationService storeIntegrationService)
        {
            _context = context;
            _storeIntegrationService = storeIntegrationService;
        }

        public async Task<Unit> Handle(UnblockStoreCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.Store entity = await _context.Stores
                .FindAsync(new object[] { request.Id }, cancellationToken);

            if (entity is null)
                throw new NotFoundException(nameof(Domain.Entities.Store), request.Id);

            entity.IsBlocked = false;

            StoreUnblockedIntegrationEvent storeBlockedEvent = new(request.Id);
            await _storeIntegrationService.SaveEventAndStoreContextChangesAsync(storeBlockedEvent);
            await _storeIntegrationService.PublishThrougEcentBusAsync(storeBlockedEvent);

            return Unit.Value;
        }
    }
}
