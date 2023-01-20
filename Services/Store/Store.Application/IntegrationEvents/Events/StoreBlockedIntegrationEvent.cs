using EventBus.Events;

namespace Store.Application.IntegrationEvents.Events
{
    public record StoreBlockedIntegrationEvent (Guid StoreId) : IntegrationEvent
    {
    }
}
