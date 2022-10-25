using AutoMapper;
using Catalog.BLL.DTO;
using Catalog.DAL.Entities;
using Type = Catalog.DAL.Entities.Type;

namespace Catalog.BLL.Mappers
{
    public class DtoToDomainMappingProfile : Profile
    {
        public DtoToDomainMappingProfile()
        {
            CreateMap<ProductDTO, Product>();
            CreateMap<BrandDTO, Brand>();
            CreateMap<TypeDTO, Type>();
        }

        public override string ProfileName => "DtoToDomainMappingProfile";
    }
}
