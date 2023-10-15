using AutoMapper;
using Catalog.BLL.DTO;
using Catalog.BLL.Services.Interfaces;
using Catalog.DAL.Entities;
using Catalog.DAL.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace Catalog.BLL.Services
{
    public class BrandService : BaseService, IBrandService
    {
        public BrandService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<BrandService> logger) 
            : base(unitOfWork, mapper, logger) { }

        public async Task<List<BrandDTO>> Get()
        {
            List<Brand> brands = await unitOfWork.BrandRepository.Find();
            return mapper.Map<List<BrandDTO>>(brands);
        }
        public async Task<BrandDTO> GetById(Guid id)
        {
            Brand brand = await unitOfWork.BrandRepository.FindById(id);
            return mapper.Map<BrandDTO>(brand);
        }
        public async Task<BrandDTO> GetLast()
        {
            Brand brand = await unitOfWork.BrandRepository.FindLast();
            return mapper.Map<BrandDTO>(brand);
        }
        public async Task Save(BrandDTO brandDTO)
        {
            if (brandDTO.Id == Guid.Empty)
            {
                var entity = mapper.Map<Brand>(brandDTO);
                unitOfWork.BrandRepository.InsertOrUpdate(entity);
                logger.LogInformation("New Brand Created");
            }
            else
            {
                Brand toEdit = await unitOfWork.BrandRepository.FindById(brandDTO.Id);
                mapper.Map(brandDTO, toEdit);
                unitOfWork.BrandRepository.InsertOrUpdate(toEdit);
                logger.LogInformation("Brand {id} updated", brandDTO.Id);
            }
            await unitOfWork.Commit();
        }
        public async Task Delete(Guid id)
        {
            Brand toDelete = await unitOfWork.BrandRepository.FindById(id);
            unitOfWork.BrandRepository.Delete(toDelete);
            await unitOfWork.Commit();
            logger.LogInformation("Brand {id} removed", id);
        }
    }
}
