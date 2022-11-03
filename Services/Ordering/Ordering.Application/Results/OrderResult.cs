using Ordering.Domain.Orders;

namespace Ordering.Application.Results
{
    public class OrderResult
    {
        public Guid Id { get; }
        public string Address { get; }
        public DateTime OrderDate { get; }
        public decimal Price { get; }
        public List<ItemResult> Items { get; }

        public OrderResult(Guid id, string address, DateTime orderDate, decimal price, List<ItemResult> items)
        {
            Id = id;
            Address = address;
            OrderDate = orderDate;
            Price = price;
            Items = items;
        }

        public OrderResult(Order order)
        {
            Id = order.Id;
            Address = order.Address.ToString();
            OrderDate = order.OrderDate;
            Price = order.GetPrice();
            Items = new();
            foreach (var item in order.Items)
            {
                ItemResult itemResult = new(item.Name, item.Units, item.UnitPrice);
                Items.Add(itemResult);
            }
        }
    }
}
