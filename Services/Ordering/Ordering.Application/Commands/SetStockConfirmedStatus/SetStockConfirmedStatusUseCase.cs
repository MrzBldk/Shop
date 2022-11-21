using Microsoft.Extensions.Logging;
using Ordering.Application.Repositories;
using Ordering.Domain.Orders;

namespace Ordering.Application.Commands.SetStockConfirmedStatus
{
    public class SetStockConfirmedStatusUseCase : ISetStockConfirmedStatusUseCase
    {
        private readonly IOrderReadOnlyRepository _orderReadOnlyRepository;
        private readonly IOrderWriteOnlyRepository _orderWriteOnlyRepository;
        private readonly ILogger _logger;

        public SetStockConfirmedStatusUseCase(IOrderReadOnlyRepository orderReadOnlyRepository,
            IOrderWriteOnlyRepository orderWriteOnlyRepository, ILogger<SetStockConfirmedStatusUseCase> logger)
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

            order.SetStockConfirmedStatus();

            _logger.LogInformation("Order {id} status changed to {newStatus}", orderId, order.OrderStatus.ToString());

            await _orderWriteOnlyRepository.Update(order);
        }
    }
}
