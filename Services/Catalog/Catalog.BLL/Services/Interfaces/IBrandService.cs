using Catalog.BLL.DTO;

namespace Catalog.BLL.Services.Interfaces
{
    public interface IBrandService
    {
        public Task<List<BrandDTO>> Get();
        public Task<BrandDTO> GetById(Guid id);
        public Task<BrandDTO> GetLast();
        public Task Save(BrandDTO brandDTO);
        public Task Delete(Guid id);

    }
}
