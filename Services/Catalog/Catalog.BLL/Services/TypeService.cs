using AutoMapper;
using Catalog.BLL.DTO;
using Catalog.BLL.Services.Interfaces;
using Catalog.DAL.Repositories.Interfaces;
using Type = Catalog.DAL.Entities.Type;

namespace Catalog.BLL.Services
{
    public class TypeService : BaseService, ITypeService
    {
        public TypeService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }

        public async Task<List<TypeDTO>> Get()
        {
            List<Type> Types = await unitOfWork.TypeRepository.Find();
            return mapper.Map<List<TypeDTO>>(Types);
        }
        public async Task<TypeDTO> GetById(Guid id)
        {
            Type Type = await unitOfWork.TypeRepository.FindById(id);
            return mapper.Map<TypeDTO>(Type);
        }
        public async Task<TypeDTO> GetLast()
        {
            Type Type = await unitOfWork.TypeRepository.FindLast();
            return mapper.Map<TypeDTO>(Type);
        }
        public async Task Save(TypeDTO typeDTO)
        {
            if (typeDTO.IsNew)
            {
                var entity = mapper.Map<Type>(typeDTO);
                unitOfWork.TypeRepository.InsertOrUpdate(entity);
            }
            else
            {
                Type toEdit = await unitOfWork.TypeRepository.FindById(typeDTO.Id);
                mapper.Map(typeDTO, toEdit);
                unitOfWork.TypeRepository.InsertOrUpdate(toEdit);
            }
            await unitOfWork.Commit();
        }
        public async Task Delete(Guid id)
        {
            Type toDelete = await unitOfWork.TypeRepository.FindById(id);
            unitOfWork.TypeRepository.Delete(toDelete);
            await unitOfWork.Commit();
        }
    }
}
