using MediatR;
using Microsoft.Extensions.Logging;
using Store.Domain.Events;

namespace Store.Application.StoreSections.EventHandlers
{
    public class SectionCreatedEventHandler : INotificationHandler<SectionCreatedEvent>
    {
        private readonly ILogger<SectionCreatedEventHandler> _logger;

        public SectionCreatedEventHandler(ILogger<SectionCreatedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(SectionCreatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Store Domain Event: {DomainEvent}", notification.GetType().Name);

            return Task.CompletedTask;
        }
    }
}
