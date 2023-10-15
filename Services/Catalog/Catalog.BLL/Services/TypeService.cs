using AutoMapper;
using Catalog.BLL.DTO;
using Catalog.BLL.Services.Interfaces;
using Catalog.DAL.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using Type = Catalog.DAL.Entities.Type;

namespace Catalog.BLL.Services
{
    public class TypeService : BaseService, ITypeService
    {
        public TypeService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<TypeService> logger) 
            : base(unitOfWork, mapper, logger) { }

        public async Task<List<TypeDTO>> Get()
        {
            List<Type> types = await unitOfWork.TypeRepository.Find();
            return mapper.Map<List<TypeDTO>>(types);
        }
        public async Task<TypeDTO> GetById(Guid id)
        {
            Type type = await unitOfWork.TypeRepository.FindById(id);
            return mapper.Map<TypeDTO>(type);
        }
        public async Task<TypeDTO> GetLast()
        {
            Type type = await unitOfWork.TypeRepository.FindLast();
            return mapper.Map<TypeDTO>(type);
        }
        public async Task Save(TypeDTO typeDTO)
        {
            if (typeDTO.Id == Guid.Empty)
            {
                var entity = mapper.Map<Type>(typeDTO);
                unitOfWork.TypeRepository.InsertOrUpdate(entity);
                logger.LogInformation("New Type Created");
            }
            else
            {
                Type toEdit = await unitOfWork.TypeRepository.FindById(typeDTO.Id);
                mapper.Map(typeDTO, toEdit);
                unitOfWork.TypeRepository.InsertOrUpdate(toEdit);
                logger.LogInformation("Type {id} updated", typeDTO.Id);
            }
            await unitOfWork.Commit();
        }
        public async Task Delete(Guid id)
        {
            Type toDelete = await unitOfWork.TypeRepository.FindById(id);
            unitOfWork.TypeRepository.Delete(toDelete);
            await unitOfWork.Commit();
            logger.LogInformation("Type {id} removed", id);
        }
    }
}
