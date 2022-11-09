using Ordering.Application.Results;
using Ordering.Domain.Orders;

namespace Ordering.Application.Commands.CreateOrder
{
    public class CreateOrderResult
    {
        public OrderResult Order { get; }
        public CreateOrderResult(Order order)
        {
            Order = new(order);
        }
    }
}
