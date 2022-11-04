using Ordering.Application.Results;

namespace Ordering.API.Model
{
    public class OrderDetailsModel
    {
        public Guid Id { get; }
        public string Address { get; }
        public DateTime OrderDate { get; }
        public decimal Price { get; }
        public List<OrderItemDetailsModel> Items { get; }

        public OrderDetailsModel(Guid id, string address, DateTime orderDate, decimal price, List<OrderItemDetailsModel> items)
        {
            Id = id;
            Address = address;
            OrderDate = orderDate;
            Price = price;
            Items = items;
        }
    }
}
