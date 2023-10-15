using Catalog.DAL.Entities;

namespace Catalog.DAL.Repositories.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : Entity
    {
        Task<List<TEntity>> Find();
        Task<TEntity> FindById(Guid id);
        Task<TEntity> FindLast();
        void InsertOrUpdate(TEntity entity);
        void Delete(TEntity entity);
    }
}
