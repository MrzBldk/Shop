namespace Ordering.Application.Commands.SetShippedStatus
{
    public interface ISetShippedStatusUseCase
    {
        public Task Execute(Guid orderId);
    }
}
