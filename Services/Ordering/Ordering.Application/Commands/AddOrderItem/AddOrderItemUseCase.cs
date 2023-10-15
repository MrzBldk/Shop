using Microsoft.Extensions.Logging;
using Ordering.Application.Repositories;
using Ordering.Domain.Orders;

namespace Ordering.Application.Commands.AddOrderItem
{
    public class AddOrderItemUseCase : IAddOrderItemUseCase
    {
        private readonly IOrderReadOnlyRepository _orderReadOnlyRepository;
        private readonly IOrderWriteOnlyRepository _orderWriteOnlyRepository;
        private readonly ILogger _logger;

        public AddOrderItemUseCase(IOrderReadOnlyRepository orderReadOnlyRepository, 
            IOrderWriteOnlyRepository orderWriteOnlyRepository, ILogger<AddOrderItemUseCase> logger)
        {
            _orderReadOnlyRepository = orderReadOnlyRepository;
            _orderWriteOnlyRepository = orderWriteOnlyRepository;
            _logger = logger;
        }

        public async Task<AddOrderItemResult> Execute(Guid orderId, Guid productId, decimal unitPrice, string name, int units = 1)
        {
            Order order = await _orderReadOnlyRepository.Get(orderId);
            if (order is null)
                throw new ApplicationException($"Order with id {orderId} not found");

            OrderItem orderItem = new(orderId, productId, unitPrice, name, units);
            order.AddItem(orderItem);

            await _orderWriteOnlyRepository.Update(order, orderItem);

            _logger.LogInformation("Item added to order {id}", orderId);

            AddOrderItemResult result = new(order);
            return result;
        }
    }
}
