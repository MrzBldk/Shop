using Catalog.DAL.Entities;
using Type = Catalog.DAL.Entities.Type;

namespace Catalog.DAL.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        public IProductRepository ProductRepository { get; }
        public IGenericRepository<Brand> BrandRepository { get; }
        public IGenericRepository<Type> TypeRepository { get; }
        public Task Commit();
    }
}
