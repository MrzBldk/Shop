using AutoMapper;
using Catalog.BLL.DTO;
using Catalog.DAL.Entities;
using Type = Catalog.DAL.Entities.Type;

namespace Catalog.BLL.Mappers
{
    public class DomainToDtoMappingProfile: Profile
    {
        public DomainToDtoMappingProfile()
        {
            CreateMap<Product, ProductDTO>();
            CreateMap<Brand, BrandDTO>();
            CreateMap<Type, TypeDTO>();
        }

        public override string ProfileName => nameof(DomainToDtoMappingProfile);

    }
}
