using EventBus.Events;

namespace Catalog.API.IntegrationEvents.Events
{
    public record StoreBlockedIntegrationEvent (Guid StoreId) : IntegrationEvent
    {
    }
}
