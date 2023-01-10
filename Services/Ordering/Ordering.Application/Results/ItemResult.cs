namespace Ordering.Application.Results
{
    public class ItemResult
    {
        public string Name { get; }
        public int Units { get; }
        public decimal UnitPrice { get; }
        public Guid ProductId { get; }

        public ItemResult(string name, int units, decimal unitPrice, Guid productId)
        {
            Name = name;
            Units = units;
            UnitPrice = unitPrice;
            ProductId = productId;
        }
    }
}
