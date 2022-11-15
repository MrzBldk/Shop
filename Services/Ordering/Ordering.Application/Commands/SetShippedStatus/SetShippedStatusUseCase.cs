using Ordering.Application.Repositories;
using Ordering.Domain.Orders;

namespace Ordering.Application.Commands.SetShippedStatus
{
    public class SetShippedStatusUseCase : ISetShippedStatusUseCase
    {
        private readonly IOrderReadOnlyRepository _orderReadOnlyRepository;
        private readonly IOrderWriteOnlyRepository _orderWriteOnlyRepository;

        public SetShippedStatusUseCase(IOrderReadOnlyRepository orderReadOnlyRepository,
            IOrderWriteOnlyRepository orderWriteOnlyRepository)
        {
            _orderReadOnlyRepository = orderReadOnlyRepository;
            _orderWriteOnlyRepository = orderWriteOnlyRepository;
        }

        public async Task Execute(Guid orderId)
        {
            Order order = await _orderReadOnlyRepository.Get(orderId);
            if (order is null)
                throw new ApplicationException($"Order with id {orderId} not found");

            order.SetShippedStatus();

            await _orderWriteOnlyRepository.Update(order);
        }
    }
}
