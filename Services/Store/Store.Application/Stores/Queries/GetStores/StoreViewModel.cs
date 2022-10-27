namespace Store.Application.Stores.Queries.GetStores
{
    public class StoreViewModel
    {
        public IList<StoreDTO> Stores { get; set; } = new List<StoreDTO>();
    }
}
