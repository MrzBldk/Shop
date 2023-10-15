using Microsoft.Extensions.Logging;
using Ordering.Application.Repositories;
using Ordering.Domain.Orders;

namespace Ordering.Application.Commands.SetCancelledStatus
{
    public class SetCancelledStatusUseCase : ISetCancelledStatusUseCase
    {
        private readonly IOrderReadOnlyRepository _orderReadOnlyRepository;
        private readonly IOrderWriteOnlyRepository _orderWriteOnlyRepository;
        private readonly ILogger _logger;

        public SetCancelledStatusUseCase(IOrderReadOnlyRepository orderReadOnlyRepository,
            IOrderWriteOnlyRepository orderWriteOnlyRepository, ILogger<SetCancelledStatusUseCase> logger)
        {
            _orderReadOnlyRepository = orderReadOnlyRepository;
            _orderWriteOnlyRepository = orderWriteOnlyRepository;
            _logger = logger;
        }

        public async Task Execute(Guid orderId)
        {
            Order order = await _orderReadOnlyRepository.Get(orderId);
            if (order is null)
                throw new ApplicationException($"Order with id {orderId} not found");

            order.SetCancelledStatus();

            _logger.LogInformation("Order {id} status changed to {newStatus}", orderId, order.OrderStatus.ToString());

            await _orderWriteOnlyRepository.Update(order);
        }
    }
}
