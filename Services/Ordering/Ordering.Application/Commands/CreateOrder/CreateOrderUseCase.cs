using Ordering.Application.Repositories;
using Ordering.Domain.Orders;
using Ordering.Domain.ValueObjects;

namespace Ordering.Application.Commands.CreateOrder
{
    public class CreateOrderUseCase : ICreateOrderUseCase
    {
        private readonly IOrderWriteOnlyRepository orderWriteOnlyRepository;


        public async Task<CreateOrderResult> Execute(string street, string city, string state, string country, string zipCode)
        {
            Address address = new(street, city, state, country, zipCode);
            Order order = new(address);
            
            await orderWriteOnlyRepository.Add(order);
            
            CreateOrderResult result = new(order);
            return result;
        }
    }
}
