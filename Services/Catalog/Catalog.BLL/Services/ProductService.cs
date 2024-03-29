﻿using AutoMapper;
using Catalog.BLL.DTO;
using Catalog.BLL.Services.Interfaces;
using Catalog.DAL.Entities;
using Catalog.DAL.Helpers;
using Catalog.DAL.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace Catalog.BLL.Services
{
    public class ProductService : BaseService, IProductService
    {
        public ProductService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<ProductService> logger)
            : base(unitOfWork, mapper, logger) { }

        public async Task<List<ProductDTO>> Get(ProductFilter filter)
        {
            List<Product> products = await unitOfWork.ProductRepository.Find(filter);
            return mapper.Map<List<ProductDTO>>(products);
        }
        public async Task<List<ProductDTO>> Get(ProductFilter filter, int skip, int take)
        {
            List<Product> products = await unitOfWork.ProductRepository.Find(filter, skip, take);
            return mapper.Map<List<ProductDTO>>(products);
        }
        public async Task<ProductDTO> GetById(Guid id)
        {
            Product product = await unitOfWork.ProductRepository.FindById(id);
            return mapper.Map<ProductDTO>(product);
        }
        public async Task<ProductDTO> GetLast()
        {
            Product product = await unitOfWork.ProductRepository.FindLast();
            return mapper.Map<ProductDTO>(product);
        }
        public async Task Save(ProductDTO productDTO)
        {
            if(productDTO.Id == Guid.Empty)
            {
                var entity = mapper.Map<Product>(productDTO);
                unitOfWork.ProductRepository.InsertOrUpdate(entity);
                logger.LogInformation("New Product Created");
            }
            else
            {
                Product toEdit = await unitOfWork.ProductRepository.FindById(productDTO.Id);
                mapper.Map(productDTO, toEdit);
                unitOfWork.ProductRepository.InsertOrUpdate(toEdit);
                logger.LogInformation("Product {id} updated", productDTO.Id);
            }
            await unitOfWork.Commit();
        }
        public async Task Delete(Guid id)
        {
            Product toDelete = await unitOfWork.ProductRepository.FindById(id);
            unitOfWork.ProductRepository.Delete(toDelete);
            await unitOfWork.Commit();
            logger.LogInformation("Product {id} removed", id);
        }
    }
}
