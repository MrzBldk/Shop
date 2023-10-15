namespace Aggregator.Models
{
    public class OrderItem
    {
        public decimal UnitPrice { get; set; }
        public string Name { get; set; }
        public int Units { get; set; }
        public Guid ProductId { get; set; }
    }
}
