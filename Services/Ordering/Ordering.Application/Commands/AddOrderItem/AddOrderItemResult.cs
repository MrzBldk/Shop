using Ordering.Application.Results;
using Ordering.Domain.Orders;

namespace Ordering.Application.Commands.AddOrderItem
{
    public class AddOrderItemResult
    {
        public OrderResult Order { get; set; }

        public AddOrderItemResult(Order order)
        {
            Order = new(order);
        }
    }
}
