using EventBus.Events;

namespace Ordering.Application.IntegrationEvents
{
    public interface IOrderingIntegrationEventService
    {
        public Task PublishThrougEventBusAsync(IntegrationEvent evt);
        public Task SaveEventAndOrderingContextChangesAsync(IntegrationEvent evt);
    }
}
