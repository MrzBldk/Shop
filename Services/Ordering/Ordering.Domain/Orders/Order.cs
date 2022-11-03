using Ordering.Domain.ValueObjects;

namespace Ordering.Domain.Orders
{
    public class Order : IEntity
    {
        public Guid Id { get; private set; }
        public Address Address { get; private set; }
        public DateTime OrderDate { get; private set; }
        
        private ItemsCollection _items;
        public IReadOnlyCollection<OrderItem> Items
        {
            get
            {
                IReadOnlyCollection<OrderItem> readOnly = _items.GetItems();
                return readOnly;
            }
        }

        public Order(Address address)
        {
            Id = Guid.NewGuid();
            Address = address;
            OrderDate = DateTime.Now;
            _items = new ItemsCollection();
        }

        private Order() { }

        public static Order Load(Guid id, Address address, ItemsCollection items, DateTime orderDate)
        {
            Order order = new()
            {
                Id = id,
                Address = address,
                _items = items,
                OrderDate = orderDate
            };
            return order;
        }

        public void AddItem(OrderItem item)
        {
            _items.Add(item);
        }

        public void AddItems(IEnumerable<OrderItem> items)
        {
            _items.Add(items);
        }

        public Price GetPrice()
        {
            return _items.GetPrice();
        }
    }
}
