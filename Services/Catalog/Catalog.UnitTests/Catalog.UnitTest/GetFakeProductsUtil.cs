using Catalog.DAL.Entities;
using Type = Catalog.DAL.Entities.Type;

namespace Catalog.UnitTest
{
    internal static class GetFakeProductsUtil
    {
        public static List<Product> GetFakeProducts()
        {

            Guid typeAId = Guid.Parse("fafbdce2-5e5d-4db4-ab7d-6553e10140fb");
            Guid typeBId = Guid.Parse("5de711ff-1001-4765-a659-ceec6132c2f5");
            Guid brandAId = Guid.Parse("a94e48d7-951d-499c-9462-7df138ab34c9");
            Guid brandBId = Guid.Parse("8e870ff1-f8f7-4f49-b692-8d9b4a1ffffc");


            Brand brandA = new()
            {
                Id = brandAId,
                Name = "A",
                Description = "Brand A"
            };
            Brand brandB = new()
            {
                Id = brandBId,
                Name = "B",
                Description = "Brand A"
            };
            Type typeA = new()
            {
                Id = typeAId,
                Name = "A",
            };
            Type typeB = new()
            {
                Id = typeBId,
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
                    BrandId = brandAId,
                    TypeId = typeAId,
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
                    BrandId = brandAId,
                    TypeId = typeBId,
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
                    BrandId = brandBId,
                    TypeId = typeAId,
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
                    BrandId = brandBId,
                    TypeId = typeBId,
                    StoreId = Guid.Empty,
                }
            };
        }
    }
}
