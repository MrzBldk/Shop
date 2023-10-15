using Catalog.BLL.DTO;
using Catalog.DAL.Helpers;

namespace Catalog.BLL.Services.Interfaces
{
    public interface IProductService
    {
        public Task<List<ProductDTO>> Get(ProductFilter filter);
        public Task<List<ProductDTO>> Get(ProductFilter filter, int skip, int take);
        public Task<ProductDTO> GetById(Guid id);
        public Task<ProductDTO> GetLast();
        public Task Save(ProductDTO productDTO);
        public Task Delete(Guid id);

    }
}
