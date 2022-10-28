namespace Store.Application.Stores.Queries.GetStores
{
    public class StoresViewModel
    {
        public IList<StoreDTO> Stores { get; set; } = new List<StoreDTO>();
    }
}
