using AutoMapper;
using Catalog.API.Controllers;
using Catalog.API.Mappers;
using Catalog.API.Models.Product;
using Catalog.BLL.Mappers;
using Catalog.BLL.Services;
using Catalog.DAL.Context;
using Catalog.DAL.Entities;
using Catalog.DAL.Helpers;
using Catalog.DAL.Repositories;
using Catalog.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Type = Catalog.DAL.Entities.Type;

namespace Catalog.UnitTests
{
    public class ProductControllerTests
    {

        private readonly DbContextOptions<CatalogContext> _dbOptions;
        private readonly Guid _typeA;
        private readonly Guid _typeB;
        private readonly Guid _brandA;
        private readonly Guid _brandB;
        MapperConfiguration _mapperConfiguration;

        public ProductControllerTests()
        {
            _dbOptions = new DbContextOptionsBuilder<CatalogContext>().UseInMemoryDatabase(databaseName: "in-memory").Options;
            using CatalogContext dbContext = new(_dbOptions);

            _typeA = Guid.NewGuid();
            _typeB = Guid.NewGuid();
            _brandA = Guid.NewGuid();
            _brandB = Guid.NewGuid();

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

            var mock = new Mock<IUnitOfWork>();
            mock.Setup(unit => unit.ProductRepository).Returns(productRepository);

            ProductService productService = new(mock.Object, mapper);
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

            ProductService productService = new(mock.Object, mapper);
            ProductController productController = new(mapper, productService);
            var actionResult = await productController.Get(0, 0, new() { Brands = new[] { _brandA } });

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

            ProductService productService = new(mock.Object, mapper);
            ProductController productController = new(mapper, productService);
            var actionResult = await productController.Get(0, 0, new() { Types = new[] { _typeA } });

            //Assert
            Assert.IsType<OkObjectResult>(actionResult);
            var actualProducts = Assert.IsAssignableFrom<List<ProductViewModel>>((actionResult as OkObjectResult).Value);
            Assert.NotEmpty(actualProducts);
            Assert.Equal(expectedLength, actualProducts.Count);
            mock.Verify(unit => unit.ProductRepository, Times.Once());
        }



        private List<Product> GetFakeProducts()
        {

            Brand brandA = new()
            {
                Id = _brandA,
                Name = "A",
                Description = "Brand A"
            };
            Brand brandB = new()
            {
                Id = _brandB,
                Name = "B",
                Description = "Brand A"
            };
            Type typeA = new()
            {
                Id = _typeA,
                Name = "A",
            };
            Type typeB = new()
            {
                Id = _typeB,
                Name = "B",
            };

            return new()
            {
                new()
                {
                    Name = "A",
                    Id = Guid.Empty,
                    Description = "Item A",
                    Price = 1,
                    PicturesUris = Array.Empty<string>(),
                    AvailableStock = 1,
                    IsArchived = false,
                    Brand = brandA,
                    Type = typeA,
                    BrandId = _brandA,
                    TypeId = _typeA,
                    StoreId = Guid.Empty
                },
                new()
                {
                    Name = "B",
                    Id = Guid.Empty,
                    Description = "Item B",
                    Price = 2,
                    PicturesUris = Array.Empty<string>(),
                    AvailableStock = 2,
                    IsArchived = false,
                    Brand = brandA,
                    Type = typeB,
                    BrandId = _brandA,
                    TypeId = _typeB,
                    StoreId = Guid.Empty,
                },
                new()
                {
                    Name = "C",
                    Id = Guid.Empty,
                    Description = "Item C",
                    Price = 3,
                    PicturesUris = Array.Empty<string>(),
                    AvailableStock = 3,
                    IsArchived = false,
                    Brand = brandB,
                    Type = typeA,
                    BrandId = _brandB,
                    TypeId = _typeA,
                    StoreId = Guid.Empty,
                },
                new()
                {
                    Name = "D",
                    Id = Guid.Empty,
                    Description = "Item D",
                    Price = 4,
                    PicturesUris = Array.Empty<string>(),
                    AvailableStock = 4,
                    IsArchived = false,
                    Brand = brandB,
                    Type = typeB,
                    BrandId = _brandB,
                    TypeId = _typeB,
                    StoreId = Guid.Empty,
                }
            };
        }
    }
}