using Ordering.Domain.Orders;

namespace Ordering.Application.Commands.AddOrderItem
{
    public interface IAddOrderItemUseCase
    {
        Task<AddOrderItemResult> Execute(Guid orderId, decimal unitPrice, string name, int units = 1);
    }
}
