namespace Basket.API.Models
{
    public partial class BasketItemDto
    {
        public string Id { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public string PictureUrl { get; set; }
    }
}