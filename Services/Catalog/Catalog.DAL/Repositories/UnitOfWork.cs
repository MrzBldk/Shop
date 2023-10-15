using Catalog.DAL.Context;
using Catalog.DAL.Entities;
using Catalog.DAL.Repositories.Interfaces;
using Type = Catalog.DAL.Entities.Type;

namespace Catalog.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private bool _disposed;
        private readonly CatalogContext _context;

        private IProductRepository _productRepository;
        private IGenericRepository<Brand> _brandRepository;
        private IGenericRepository<Type> _typeRepository;
        public IProductRepository ProductRepository => _productRepository ??= new ProductRepository(_context);
        public IGenericRepository<Brand> BrandRepository => _brandRepository ??= new GenericRepository<Brand>(_context);
        public IGenericRepository<Type> TypeRepository => _typeRepository ??= new GenericRepository<Type>(_context);

        public UnitOfWork(CatalogContext context)
        {
            _context = context;
        }
        
        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                _disposed = true;
            }
        }
    }
}
