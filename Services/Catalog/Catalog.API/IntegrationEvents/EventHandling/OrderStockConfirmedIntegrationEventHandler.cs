using Catalog.API.IntegrationEvents.Events;
using Catalog.DAL.Context;
using EventBus.Abstractions;

namespace Catalog.API.IntegrationEvents.EventHandling
{
    public class OrderStockConfirmedIntegrationEventHandler : IIntegrationEventHandler<OrderStockConfirmedIntegrationEvent>
    {
        private readonly CatalogContext _catalogContext;
        private readonly ILogger<OrderStockConfirmedIntegrationEventHandler> _logger;

        public OrderStockConfirmedIntegrationEventHandler(CatalogContext catalogContext,
            ILogger<OrderStockConfirmedIntegrationEventHandler> logger)
        {
            _catalogContext = catalogContext;
            _logger = logger;
        }

        public async Task Handle(OrderStockConfirmedIntegrationEvent @event)
        {

            _logger.LogInformation("----- Handling integration event: {IntegrationEventId} at {AppName} - ({@IntegrationEvent})", @event.Id, "Catalog", @event);

            foreach (var orderStockItem in @event.OrderItems)
            {
                var catalogItem = _catalogContext.Products.Find(orderStockItem.Id);
                if (catalogItem != null)
                    catalogItem.AvailableStock -= orderStockItem.Quantity;
            }

            _catalogContext.SaveChanges();
        }
    }
}
