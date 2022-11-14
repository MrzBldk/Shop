namespace Ordering.Application.Commands.SetStockConfirmedStatus
{
    public interface ISetStockConfirmedStatusUseCase
    {
        public Task Execute(Guid orderId);
    }
}
