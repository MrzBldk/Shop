namespace Ordering.Application.Commands.SetAwaitingValidationStatus
{
    public interface ISetAwaitingValidationStatusUseCase
    {
        public Task Execute(Guid orderId);
    }
}
