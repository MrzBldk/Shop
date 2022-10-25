using Catalog.DAL.Context;
using Catalog.DAL.Entities;
using Catalog.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.DAL.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : Entity
    {
        internal CatalogContext context;
        internal DbSet<TEntity> dbSet;

        public GenericRepository(CatalogContext context)
        {
            this.context = context;
            dbSet = context.Set<TEntity>();
        }
        public virtual async Task<List<TEntity>> Find()
        {
            return await dbSet.ToListAsync();
        }

        public virtual async Task<TEntity> FindById(Guid id)
        {
            return await dbSet.FindAsync(id);
        }

        public virtual Task<TEntity> FindLast()
        {
            return dbSet.OrderByDescending(TEntity => TEntity.Created).FirstAsync();
        }

        public virtual void InsertOrUpdate(TEntity entity)
        {
            if (entity.IsNew)
                dbSet.Add(entity);
            else
                dbSet.Update(entity);
        }

        public virtual void Delete(TEntity entity)
        {
            dbSet.Remove(entity);
        }
    }
}
