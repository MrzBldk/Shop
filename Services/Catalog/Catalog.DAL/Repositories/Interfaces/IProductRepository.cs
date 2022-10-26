using Catalog.DAL.Entities;
using Catalog.DAL.Helpers;

namespace Catalog.DAL.Repositories.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        public Task<List<Product>> Find(ProductFilter filter);
        public Task<List<Product>> Find(ProductFilter filter, int skip, int take);
    }
}
