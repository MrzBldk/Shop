namespace Aggregator.Models
{
    public class UpdateBasketItemsRequest
    {

        public string BasketId { get; set; }

        public ICollection<UpdateBasketItemData> Updates { get; set; }

        public UpdateBasketItemsRequest()
        {
            Updates = new List<UpdateBasketItemData>();
        }
    }

    public class UpdateBasketItemData
    {
        public string BasketItemId { get; set; }
        public int NewQuantity { get; set; }

        public UpdateBasketItemData()
        {
            NewQuantity = 0;
        }
    }
}
