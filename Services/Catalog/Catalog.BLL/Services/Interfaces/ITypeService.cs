using Catalog.BLL.DTO;

namespace Catalog.BLL.Services.Interfaces
{
    public interface ITypeService
    {
        public Task<List<TypeDTO>> Get();
        public Task<TypeDTO> GetById(Guid id);
        public Task<TypeDTO> GetLast();
        public Task Save(TypeDTO typeDTO);
        public Task Delete(Guid id);
    }
}
