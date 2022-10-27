using Store.Application.Common.Mappings;

namespace Store.Application.Stores.Queries.GetStores
{
    public class StoreDTO : IMapFrom<Domain.Entities.Store>
    {
        public StoreDTO()
        {
            Sections = new();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

        public List<StoreSectionDTO> Sections { get; set; }
    }
}
