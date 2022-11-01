using MediatR;
using Microsoft.Extensions.Logging;
using Store.Domain.Events;

namespace Store.Application.StoreSections.EventHandlers
{
    public class SectionDeletedEventHandler : INotificationHandler<SectionDeletedEvent>
    {
        private readonly ILogger<SectionDeletedEventHandler> _logger;

        public SectionDeletedEventHandler(ILogger<SectionDeletedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(SectionDeletedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Store Domain Event: {DomainEvent}", notification.GetType().Name);

            return Task.CompletedTask;
        }
    }
}
