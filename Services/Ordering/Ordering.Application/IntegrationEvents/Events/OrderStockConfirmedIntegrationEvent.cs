using EventBus.Events;
using Ordering.Domain.Orders;

namespace Ordering.Application.IntegrationEvents.Events
{
    public record OrderStockConfirmedIntegrationEvent : IntegrationEvent
    {
        public Guid OrderId { get;}
        public IEnumerable<ProductData> OrderItems { get; }

        public OrderStockConfirmedIntegrationEvent(Guid orderId, IReadOnlyCollection<OrderItem> orderItems)
        {
            OrderId = orderId;
            List<ProductData> items = new();
            foreach(var item in orderItems)
            {
                items.Add(new(item.OrderId, item.Units));
            }
            OrderItems = items;
        }
    }

    public record ProductData(Guid Id, int Quantity) { }
}
