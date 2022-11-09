namespace Ordering.Application.Results
{
    public class ItemResult
    {
        public string Name { get; }
        public int Units { get; }
        public decimal UnitPrice { get; }

        public ItemResult(string name, int units, decimal unitPrice)
        {
            Name = name;
            Units = units;
            UnitPrice = unitPrice;
        }
    }
}
