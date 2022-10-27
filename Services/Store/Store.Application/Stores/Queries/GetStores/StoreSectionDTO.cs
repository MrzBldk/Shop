using AutoMapper;
using Store.Application.Common.Mappings;
using Store.Domain.Entities;

namespace Store.Application.Stores.Queries.GetStores
{
    public class StoreSectionDTO : IMapFrom<StoreSection>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public int Priority { get; set; }

        public Guid StoreId;

        public void Mapping(Profile profile)
        {
            profile.CreateMap<StoreSection, StoreSectionDTO>()
                .ForMember(d => d.Priority, opt => opt.MapFrom(s => (int)s.Priority));
        }
    }
}
