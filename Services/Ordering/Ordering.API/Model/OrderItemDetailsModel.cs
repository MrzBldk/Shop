namespace Ordering.API.Model
{
    public class OrderItemDetailsModel
    {
        public string Name { get; }
        public int Units { get; }
        public decimal UnitPrice { get; }
        public Guid ProductId { get; set; }

        public OrderItemDetailsModel(string name, int units, decimal unitPrice, Guid productId)
        {
            Name = name;
            Units = units;
            UnitPrice = unitPrice;
            ProductId = productId;
        }
    }
}
