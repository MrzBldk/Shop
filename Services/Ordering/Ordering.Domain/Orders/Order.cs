using Ordering.Domain.ValueObjects;

namespace Ordering.Domain.Orders
{
    public class Order : IEntity
    {
        public Guid Id { get; private set; }
        public Address Address { get; private set; }
        public DateTime OrderDate { get; private set; }
        public OrderStatus OrderStatus { get; private set; }

        private int _orderStatusId;
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
            _orderStatusId = OrderStatus.Submitted.Id;
            OrderStatus = OrderStatus.From(_orderStatusId);
        }

        private Order() { }

        public static Order Load(Guid id, Address address, ItemsCollection items, DateTime orderDate, int orderStatus)
        {
            Order order = new()
            {
                Id = id,
                Address = address,
                _items = items,
                OrderDate = orderDate,
                _orderStatusId = orderStatus,
                OrderStatus = OrderStatus.Submitted
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

        public void SetAwaitingValidationStatus()
        {
            if (_orderStatusId == OrderStatus.Submitted.Id)
            {
                _orderStatusId = OrderStatus.AwaitingValidation.Id;
                OrderStatus = OrderStatus.From(_orderStatusId);
            }
        }

        public void SetStockConfirmedStatus()
        {
            if (_orderStatusId == OrderStatus.AwaitingValidation.Id)
            {
                _orderStatusId = OrderStatus.StockConfirmed.Id;
                OrderStatus = OrderStatus.From(_orderStatusId);
            }
        }

        public void SetShippedStatus()
        {
            if (_orderStatusId != OrderStatus.StockConfirmed.Id)
            {
                StatusChangeException(OrderStatus.Shipped);
            }
            _orderStatusId = OrderStatus.Shipped.Id;
            OrderStatus = OrderStatus.From(_orderStatusId);
        }

        public void SetCancelledStatus()
        {
            if (_orderStatusId == OrderStatus.Shipped.Id ||
                _orderStatusId == OrderStatus.AwaitingValidation.Id)
            {
                StatusChangeException(OrderStatus.Cancelled);
            }

            _orderStatusId = OrderStatus.Cancelled.Id;
            OrderStatus = OrderStatus.From(_orderStatusId);
        }

        private void StatusChangeException(OrderStatus orderStatusToChange)
        {
            throw new DomainException($"Is not possible to change the order status from {OrderStatus.Name} to {orderStatusToChange.Name}.");
        }
    }
}
