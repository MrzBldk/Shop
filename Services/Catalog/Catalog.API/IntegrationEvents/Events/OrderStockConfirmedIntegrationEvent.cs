using EventBus.Events;

namespace Catalog.API.IntegrationEvents.Events
{
    public record OrderStockConfirmedIntegrationEvent : IntegrationEvent
    {
        public Guid OrderId { get;}
        public IEnumerable<ProductData> OrderItems { get; }

        public OrderStockConfirmedIntegrationEvent(Guid orderId, List<ProductData> orderItems)
        {
            OrderId = orderId;
            OrderItems = orderItems;
        }
    }

    public record ProductData(Guid Id, int Quantity) { }
}
