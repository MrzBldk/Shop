using EventBus.Events;

namespace Store.Application.IntegrationEvents
{
    public interface IStoreIntegrationService
    {
        public Task PublishThrougEcentBusAsync(IntegrationEvent evt);
        public Task SaveEventAndStoreContextChangesAsync(IntegrationEvent evt);
    }
}
