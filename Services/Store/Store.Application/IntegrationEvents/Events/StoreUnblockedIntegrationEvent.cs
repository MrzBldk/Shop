using EventBus.Events;

namespace Store.Application.IntegrationEvents.Events
{
    public record StoreUnblockedIntegrationEvent(Guid StoreId) : IntegrationEvent
    {
    }
}
