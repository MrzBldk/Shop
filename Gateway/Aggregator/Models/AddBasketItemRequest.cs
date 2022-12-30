namespace Aggregator.Models
{
    public class AddBasketItemRequest
    {
        public string CatalogItemId { get; set; }
        public string BasketId { get; set; }

        public int Quantity { get; set; }

        public AddBasketItemRequest()
        {
            Quantity = 1;
        }
    }
}
