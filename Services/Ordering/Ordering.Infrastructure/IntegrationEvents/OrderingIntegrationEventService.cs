using EventBus.Abstractions;
using EventBus.Events;
using IntegrationEventLogEF.Services;
using IntegrationEventLogEF.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Ordering.Application.IntegrationEvents;
using Ordering.Infrastructure.DataAccess;
using System.Data.Common;

namespace Ordering.Infrastructure.IntegrationEvents
{
    public class OrderingIntegrationEventService : IOrderingIntegrationEventService
    {
        private readonly Func<DbConnection, IIntegrationEventLogService> _integrationEventLogServiceFactory;
        private readonly IEventBus _eventBus;
        private readonly Context _orderingContext;
        private readonly IIntegrationEventLogService _eventLogService;
        private readonly ILogger<OrderingIntegrationEventService> _logger;
        private volatile bool disposedValue;

        public OrderingIntegrationEventService(
            Func<DbConnection, IIntegrationEventLogService> integrationEventLogServiceFactory,
            IEventBus eventBus,
            Context storeContext,
            ILogger<OrderingIntegrationEventService> logger)
        {
            _integrationEventLogServiceFactory = integrationEventLogServiceFactory;
            _eventBus = eventBus;
            _orderingContext = storeContext;
            _eventLogService = _integrationEventLogServiceFactory(_orderingContext.Database.GetDbConnection());
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

        public async Task SaveEventAndOrderingContextChangesAsync(IntegrationEvent evt)
        {

            _logger.LogInformation("----- CatalogIntegrationEventService - Saving changes and integrationEvent: {IntegrationEventId}", evt.Id);

            await ResilientTransaction.New(_orderingContext).ExecuteAsync(async () =>
            {
                await _orderingContext.SaveChangesAsync();
                await _eventLogService.SaveEventAsync(evt, _orderingContext.Database.CurrentTransaction);
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
