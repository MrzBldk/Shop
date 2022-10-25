using AutoMapper;
using Catalog.API.Models.Brand;
using Catalog.API.Models.Product;
using Catalog.API.Models.Type;
using Catalog.BLL.DTO;

namespace Catalog.API.Mappers
{
    public class DtoToViewModelMappingProfile : Profile
    {
        public DtoToViewModelMappingProfile()
        {
            CreateMap<ProductDTO, ProductViewModel>()
                .ForMember(d => d.Brand, opts => opts.MapFrom(s => s.Brand.Name))
                .ForMember(d => d.Type, opts => opts.MapFrom(s => s.Type.Name));
            
            CreateMap<BrandDTO, BrandViewModel>();
            CreateMap<TypeDTO, TypeViewModel>();
        }

        public override string ProfileName => "DtoToViewModelMappingProfile";
    }
}
