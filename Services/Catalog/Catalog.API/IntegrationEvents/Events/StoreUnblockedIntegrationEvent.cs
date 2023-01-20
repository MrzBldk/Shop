using EventBus.Events;

namespace Catalog.API.IntegrationEvents.Events
{
    public record StoreUnblockedIntegrationEvent(Guid StoreId) : IntegrationEvent
    {
    }
}
