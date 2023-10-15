using Catalog.DAL.Context;
using Catalog.DAL.Entities;
using Catalog.DAL.Helpers;
using Catalog.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog.DAL.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(CatalogContext context) : base(context) { }

        private IQueryable<Product> Filter(ProductFilter filter)
        {
            IQueryable<Product> products = dbSet.Where(p => !p.IsArchived);

            if (filter.Types is not null && filter.Types.Any())
                products = products.Where(p => filter.Types.Contains(p.TypeId));

            if (filter.Brands is not null && filter.Brands.Any())
                products = products.Where(p => filter.Brands.Contains(p.BrandId));

            if (filter.Stores is not null && filter.Stores.Any())
                products = products.Where(p => filter.Stores.Contains(p.StoreId));

            return products.Include(p => p.Brand).Include(p => p.Type);
        }

        public async Task<List<Product>> Find(ProductFilter filter)
        {
            return await Filter(filter).ToListAsync();
        }

        public Task<List<Product>> Find(ProductFilter filter, int skip, int take)
        {
            return Filter(filter).Skip(skip).Take(take).ToListAsync();
        }

        public override async Task<Product> FindById(Guid id)
        {
            return await dbSet
                .Include(p => p.Brand)
                .Include(p => p.Type)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
