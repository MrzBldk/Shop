using EventBus.Abstractions;
using EventBus.Events;
using IntegrationEventLogEF.Services;
using IntegrationEventLogEF.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Store.Application.IntegrationEvents;
using Store.Infrasructure.Persistence;
using System.Data.Common;

namespace Store.Infrasructure.IntegrationEvents
{
    public class StoreIntegrationEventService : IStoreIntegrationService, IDisposable
    {
        private readonly Func<DbConnection, IIntegrationEventLogService> _integrationEventLogServiceFactory;
        private readonly IEventBus _eventBus;
        private readonly StoreDbContext _storeContext;
        private readonly IIntegrationEventLogService _eventLogService;
        private readonly ILogger<StoreIntegrationEventService> _logger;
        private volatile bool disposedValue;

        public StoreIntegrationEventService(
            Func<DbConnection, IIntegrationEventLogService> integrationEventLogServiceFactory, 
            IEventBus eventBus,
            StoreDbContext storeContext, 
            ILogger<StoreIntegrationEventService> logger)
        {
            _integrationEventLogServiceFactory = integrationEventLogServiceFactory;
            _eventBus = eventBus;
            _storeContext = storeContext;
            _eventLogService = _integrationEventLogServiceFactory(_storeContext.Database.GetDbConnection());
            _logger = logger;
        }

        public async Task PublishThrougEcentBusAsync(IntegrationEvent evt)
        {
            try
            {
                _logger.LogInformation("----- Publishing integration event: {IntegrationEventId_published} from {AppName} - ({@IntegrationEvent})", evt.Id, "Store", evt);

                await _eventLogService.MarkEventAsInProgressAsync(evt.Id);
                _eventBus.Publish(evt);
                await _eventLogService.MarkEventAsPublishedAsync(evt.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ERROR Publishing integration event: {IntegrationEventId} from {AppName} - ({@IntegrationEvent})", evt.Id, "Store", evt);
                await _eventLogService.MarkEventAsFailedAsync(evt.Id);
            }
        }

        public async Task SaveEventAndStoreContextChangesAsync(IntegrationEvent evt)
        {
            _logger.LogInformation("----- CatalogIntegrationEventService - Saving changes and integrationEvent: {IntegrationEventId}", evt.Id);
 
            await ResilientTransaction.New(_storeContext).ExecuteAsync(async () =>
            {
                await _storeContext.SaveChangesAsync();
                await _eventLogService.SaveEventAsync(evt, _storeContext.Database.CurrentTransaction);
            });
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    (_eventLogService as IDisposable)?.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
