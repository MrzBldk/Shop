using Ordering.Domain.ValueObjects;

namespace Ordering.Application.Commands.CreateOrder
{
    public interface ICreateOrderUseCase
    {
        Task<CreateOrderResult> Execute(string street, string city, string state, string country, string zipCode);
    }
}
