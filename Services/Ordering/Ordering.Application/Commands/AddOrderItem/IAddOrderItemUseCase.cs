namespace Ordering.Application.Commands.AddOrderItem
{
    public interface IAddOrderItemUseCase
    {
        Task<AddOrderItemResult> Execute(Guid orderId, Guid productId, decimal unitPrice, string name, int units = 1);
    }
}
