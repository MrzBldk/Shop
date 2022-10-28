namespace Store.Application.Stores.Queries.GetStore
{
    public class StoreViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

        public IList<StoreSectionDTO> Sections { get; set; } = new List<StoreSectionDTO>();
    }
}
