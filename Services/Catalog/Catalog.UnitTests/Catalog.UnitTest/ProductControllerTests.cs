using AutoMapper;
using Castle.Core.Logging;
using Catalog.API.Controllers;
using Catalog.API.Mappers;
using Catalog.API.Models.Product;
using Catalog.BLL.Mappers;
using Catalog.BLL.Services;
using Catalog.DAL.Context;
using Catalog.DAL.Repositories;
using Catalog.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using static Catalog.UnitTest.GetFakeProductsUtil;

namespace Catalog.UnitTests
{
    public class ProductControllerTests
    {

        private readonly DbContextOptions<CatalogContext> _dbOptions;
        private readonly MapperConfiguration _mapperConfiguration;

        public ProductControllerTests()
        {
            _dbOptions = new DbContextOptionsBuilder<CatalogContext>().UseInMemoryDatabase(databaseName: "in-memory").Options;
            using CatalogContext dbContext = new(_dbOptions);

            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();

            dbContext.Products.AddRange(GetFakeProducts());

            dbContext.SaveChanges();

            Profile[] profiles = new[] {
                new DtoToDomainMappingProfile() as Profile,
                new DomainToDtoMappingProfile() as Profile,
                new DtoToViewModelMappingProfile() as Profile,
                new ViewModelToDtoMappingProfile() as Profile
            };
            _mapperConfiguration = new(cfg => cfg.AddProfiles(profiles));
        }

        [Theory]
        [InlineData(0, 0, 4)]
        [InlineData(1, 1, 1)]
        [InlineData(1, 2, 2)]
        public async Task Get_products_success(int skip, int take, int expected)
        {
            //Arrange
            var expectedLength = expected;

            //Act
            CatalogContext catalogContext = new(_dbOptions);
            ProductRepository productRepository = new(catalogContext);
            Mapper mapper = new(_mapperConfiguration);

            Mock<IUnitOfWork> mock = new();
            mock.Setup(unit => unit.ProductRepository).Returns(productRepository);
            Mock<ILogger<ProductService>> loggerMock = new();

            ProductService productService = new(mock.Object, mapper, loggerMock.Object);
            ProductController productController = new(mapper, productService);
            var actionResult = await productController.Get(skip, take, new());

            //Assert
            Assert.IsType<OkObjectResult>(actionResult);
            var actualProducts = Assert.IsAssignableFrom<List<ProductViewModel>>((actionResult as OkObjectResult).Value);
            Assert.NotEmpty(actualProducts);
            Assert.Equal(expectedLength, actualProducts.Count);
            mock.Verify(unit => unit.ProductRepository, Times.Once());
        }

        [Fact]
        public async Task Get_filteredByBrand_products_success()
        {
            //Arrange
            var expectedLength = 2;

            //Act
            CatalogContext catalogContext = new(_dbOptions);
            ProductRepository productRepository = new(catalogContext);
            Mapper mapper = new(_mapperConfiguration);

            var mock = new Mock<IUnitOfWork>();
            mock.Setup(unit => unit.ProductRepository).Returns(productRepository);
            Mock<ILogger<ProductService>> loggerMock = new();

            ProductService productService = new(mock.Object, mapper, loggerMock.Object);
            ProductController productController = new(mapper, productService);
            var actionResult = await productController.Get(0, 0, new() { Brands = new[] { Guid.Parse("a94e48d7-951d-499c-9462-7df138ab34c9") } });

            //Assert
            Assert.IsType<OkObjectResult>(actionResult);
            var actualProducts = Assert.IsAssignableFrom<List<ProductViewModel>>((actionResult as OkObjectResult).Value);
            Assert.NotEmpty(actualProducts);
            Assert.Equal(expectedLength, actualProducts.Count);
            mock.Verify(unit => unit.ProductRepository, Times.Once());
        }

        [Fact]
        public async Task Get_products_filteredByType_success()
        {
            //Arrange
            var expectedLength = 2;

            //Act
            CatalogContext catalogContext = new(_dbOptions);
            ProductRepository productRepository = new(catalogContext);
            Mapper mapper = new(_mapperConfiguration);

            var mock = new Mock<IUnitOfWork>();
            mock.Setup(unit => unit.ProductRepository).Returns(productRepository);
            Mock<ILogger<ProductService>> loggerMock = new();

            ProductService productService = new(mock.Object, mapper, loggerMock.Object);
            ProductController productController = new(mapper, productService);
            var actionResult = await productController.Get(0, 0, new() { Types = new[] { Guid.Parse("fafbdce2-5e5d-4db4-ab7d-6553e10140fb") } });

            //Assert
            Assert.IsType<OkObjectResult>(actionResult);
            var actualProducts = Assert.IsAssignableFrom<List<ProductViewModel>>((actionResult as OkObjectResult).Value);
            Assert.NotEmpty(actualProducts);
            Assert.Equal(expectedLength, actualProducts.Count);
            mock.Verify(unit => unit.ProductRepository, Times.Once());
        }
    }
}