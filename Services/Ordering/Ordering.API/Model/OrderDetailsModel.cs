namespace Ordering.API.Model
{
    public class OrderDetailsModel
    {
        public Guid Id { get; }
        public string Address { get; }
        public DateTime OrderDate { get; }
        public decimal Price { get; }
        public List<OrderItemDetailsModel> Items { get; }
        public string Status { get; }
        public Guid UserId { get; }

        public OrderDetailsModel(Guid id, string address, DateTime orderDate, decimal price, List<OrderItemDetailsModel> items, string status, Guid userId)
        {
            Id = id;
            Address = address;
            OrderDate = orderDate;
            Price = price;
            Items = items;
            Status = status;
            UserId = userId;
        }
    }
}
