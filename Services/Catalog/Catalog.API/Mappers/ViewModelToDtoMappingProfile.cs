using AutoMapper;
using Catalog.API.Models.Brand;
using Catalog.API.Models.Product;
using Catalog.API.Models.Type;
using Catalog.BLL.DTO;

namespace Catalog.API.Mappers
{
    public class ViewModelToDtoMappingProfile : Profile
    {
        public ViewModelToDtoMappingProfile()
        {
            CreateMap<CreateProductViewModel, ProductDTO>()
                .ForMember(d => d.IsArchived, opts => opts.MapFrom(s => false));

            CreateMap<UpdateProductViewModel, ProductDTO>();
            CreateMap<BrandViewModel, BrandDTO>();
            CreateMap<CreateBrandViewModel, BrandDTO>();
            CreateMap<TypeViewModel, TypeDTO>();
            CreateMap<CreateTypeViewModel, TypeDTO>();
        }

        public override string ProfileName => "ViewModelToDtoMappingProfile";
    }
}
