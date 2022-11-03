using Ordering.Application.Repositories;
using Ordering.Domain.Orders;

namespace Ordering.Application.Commands.AddOrderItem
{
    public class AddOrderItemUseCase : IAddOrderItemUseCase
    {
        private readonly IOrderReadOnlyRepository orderReadOnlyRepository;
        private readonly IOrderWriteOnlyRepository orderWriteOnlyRepository; 
        public async Task<AddOrderItemResult> Execute(Guid orderId, decimal unitPrice, string name, int units = 1)
        {
            Order order = await orderReadOnlyRepository.Get(orderId);
            if (order is null)
                throw new ApplicationException($"Order with id {orderId} not found");

            OrderItem orderItem = new(orderId, unitPrice, name, units);
            order.AddItem(orderItem);

            await orderWriteOnlyRepository.Update(order, orderItem);

            AddOrderItemResult result = new(order);
            return result;
        }
    }
}
