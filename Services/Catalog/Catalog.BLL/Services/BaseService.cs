using AutoMapper;
using Catalog.DAL.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace Catalog.BLL.Services
{
    public abstract class BaseService
    {
        protected readonly IUnitOfWork unitOfWork;
        protected readonly IMapper mapper;
        protected readonly ILogger logger;

        protected BaseService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<BaseService> logger)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.logger = logger;
        }
    }
}
