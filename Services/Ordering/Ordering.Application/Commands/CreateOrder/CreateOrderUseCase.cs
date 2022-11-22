using Microsoft.Extensions.Logging;
using Ordering.Application.Repositories;
using Ordering.Domain.Orders;
using Ordering.Domain.ValueObjects;

namespace Ordering.Application.Commands.CreateOrder
{
    public class CreateOrderUseCase : ICreateOrderUseCase
    {
        private readonly IOrderWriteOnlyRepository _orderWriteOnlyRepository;
        private readonly ILogger _logger;

        public CreateOrderUseCase(IOrderWriteOnlyRepository orderWriteOnlyRepository, ILogger<CreateOrderUseCase> logger)
        {
            _orderWriteOnlyRepository = orderWriteOnlyRepository;
            _logger = logger;
        }

        public async Task<CreateOrderResult> Execute(Address address)
        {
            Order order = new(address);
            
            await _orderWriteOnlyRepository.Add(order);

            _logger.LogInformation("New order created");
            
            CreateOrderResult result = new(order);
            return result;
        }
    }
}
