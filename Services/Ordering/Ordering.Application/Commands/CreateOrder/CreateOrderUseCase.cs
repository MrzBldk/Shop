using Ordering.Application.Repositories;
using Ordering.Domain.Orders;
using Ordering.Domain.ValueObjects;

namespace Ordering.Application.Commands.CreateOrder
{
    public class CreateOrderUseCase : ICreateOrderUseCase
    {
        private readonly IOrderWriteOnlyRepository _orderWriteOnlyRepository;

        public CreateOrderUseCase(IOrderWriteOnlyRepository orderWriteOnlyRepository)
        {
            _orderWriteOnlyRepository = orderWriteOnlyRepository;
        }

        public async Task<CreateOrderResult> Execute(Address address)
        {
            Order order = new(address);
            
            await _orderWriteOnlyRepository.Add(order);
            
            CreateOrderResult result = new(order);
            return result;
        }
    }
}
