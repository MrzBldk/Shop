using Catalog.API.IntegrationEvents.Events;
using Catalog.DAL.Context;
using EventBus.Abstractions;

namespace Catalog.API.IntegrationEvents.EventHandling
{
    public class StoreBlockedIntegrationEventHandler : IIntegrationEventHandler<StoreBlockedIntegrationEvent>
    {
        private readonly CatalogContext _catalogContext;
        private readonly ILogger<OrderStockConfirmedIntegrationEventHandler> _logger;

        public StoreBlockedIntegrationEventHandler(CatalogContext catalogContext,
            ILogger<OrderStockConfirmedIntegrationEventHandler> logger)
        {
            _catalogContext = catalogContext;
            _logger = logger;
        }

        public async Task Handle(StoreBlockedIntegrationEvent @event)
        {

            _logger.LogInformation("----- Handling integration event: {IntegrationEventId} at {AppName} - ({@IntegrationEvent})", @event.Id, "Catalog", @event);

            var catalogItems = _catalogContext.Products.Where(p => p.StoreId == @event.StoreId);

            foreach(var catalogItem in catalogItems)
            {
                catalogItem.IsArchived = true;
            }

            _catalogContext.SaveChanges();
        }
    }
}
