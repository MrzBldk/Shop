using Ordering.Domain.ValueObjects;
using System.Collections.ObjectModel;

namespace Ordering.Domain.Orders
{
    public class ItemsCollection
    {
        private readonly IList<OrderItem> _items;

        public IReadOnlyCollection<OrderItem> GetItems()
        {
            var result = new ReadOnlyCollection<OrderItem>(_items);
            return result;
        }
        public ItemsCollection()
        {
            _items = new List<OrderItem>();
        }

        public void Add(OrderItem item)
        {
            _items.Add(item);
        }

        public void Add(IEnumerable<OrderItem> items)
        {
            foreach (var transaction in items)
            {
                Add(transaction);
            }
        }
        public Price GetPrice()
        {
            Price totalPrice = 0;
            foreach (var item in _items)
            {
                totalPrice += item.UnitPrice * item.Units;
            }
            return totalPrice;
        }
    }
}
