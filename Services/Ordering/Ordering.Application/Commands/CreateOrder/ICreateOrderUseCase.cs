using Ordering.Domain.ValueObjects;

namespace Ordering.Application.Commands.CreateOrder
{
    public interface ICreateOrderUseCase
    {
        Task<CreateOrderResult> Execute(Address address, string userId);
    }
}
