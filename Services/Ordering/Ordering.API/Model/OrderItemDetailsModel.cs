namespace Ordering.API.Model
{
    public class OrderItemDetailsModel
    {
        public string Name { get; }
        public int Units { get; }
        public decimal UnitPrice { get; }

        public OrderItemDetailsModel(string name, int units, decimal unitPrice)
        {
            Name = name;
            Units = units;
            UnitPrice = unitPrice;
        }
    }
}
