using Ordering.Domain.ValueObjects;

namespace Ordering.Domain.Orders
{
    public class OrderItem : IEntity, IAggregateRoot
    {
        public Guid Id { get; private set; }
        public int Units { get; private set; }
        public Price UnitPrice { get; private set; }
        public string Name { get; private set; }
        public Guid ProductId { get; set; }


        public Guid OrderId { get; set; }

        public OrderItem(Guid orderId, Guid productId, decimal unitPrice, string name, int units = 1)
        {
            Id = Guid.NewGuid();
            Units = units;
            UnitPrice = unitPrice;
            Name = name;
            OrderId = orderId;
            ProductId = productId;
        }

        private OrderItem() { }

        public static OrderItem Load(Guid id, Guid orderId, Guid productId, int units, decimal unitPrice, string name)
        {
            OrderItem item = new()
            {
                Id = id,
                OrderId = orderId,
                Units = units,
                UnitPrice = unitPrice,
                Name = name,
                ProductId = productId
            };

            return item;
        }

        public void AddUnits(int units)
        {
            if (units < 0)
                throw new DomainException("Invalid units");

            Units += units;
        }
    }
}
