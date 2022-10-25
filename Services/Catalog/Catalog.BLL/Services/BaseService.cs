using AutoMapper;
using Catalog.BLL.Services.Interfaces;
using Catalog.DAL.Repositories.Interfaces;

namespace Catalog.BLL.Services
{
    public abstract class BaseService
    {
        protected readonly IUnitOfWork unitOfWork;
        protected readonly IMapper mapper;

        protected BaseService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
    }
}
