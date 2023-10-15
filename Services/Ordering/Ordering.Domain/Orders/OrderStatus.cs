namespace Ordering.Domain.Orders
{
    public class OrderStatus : Enumeration
    {
        public static OrderStatus Submitted = new(1, nameof(Submitted).ToLowerInvariant());
        public static OrderStatus AwaitingValidation = new(2, nameof(AwaitingValidation).ToLowerInvariant());
        public static OrderStatus StockConfirmed = new(3, nameof(StockConfirmed).ToLowerInvariant());
        public static OrderStatus Shipped = new(4, nameof(Shipped).ToLowerInvariant());
        public static OrderStatus Cancelled = new(5, nameof(Cancelled).ToLowerInvariant());

        public OrderStatus(int id, string name) : base(id, name) { }

        public static IEnumerable<OrderStatus> List() =>
            new[] { Submitted, AwaitingValidation, StockConfirmed, Shipped, Cancelled };

        public static OrderStatus FromName(string name)
        {
            OrderStatus state = List()
                .SingleOrDefault(s => string.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if (state is null)
                throw new DomainException($"Possible values for OrderStatus: {string.Join(",", List().Select(s => s.Name))}");

            return state;
        }

        public static OrderStatus From(int id)
        {
            var state = List().SingleOrDefault(s => s.Id == id);

            if (state is null)
                throw new DomainException($"Possible values for OrderStatus: {string.Join(",", List().Select(s => s.Name))}");

            return state;
        }
    }
}
