using Ordering.Domain.ValueObjects;

namespace Ordering.Infrastructure.DataAccess.Entities
{
    public class OrderItem
    {
        public Guid Id { get; set; }
        public int Units { get; set; }
        public decimal UnitPrice { get; set; }
        public string Name { get; set; }
        public Guid OrderId { get; set; }
    }
}
