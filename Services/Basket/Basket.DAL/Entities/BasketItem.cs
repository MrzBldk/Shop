namespace Basket.DAL.Entities
{
    public class BasketItem
    {
        public string Id { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public string PictureUrl { get; set; }
    }
}
