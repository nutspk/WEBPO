using WEBPO.Domain.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WEBPO.Domain.UnitOfWork.Collections;
using System.Threading;

namespace WEBPO.Domain.Repositories
{
    public class Repository<TEntity> : BaseRepository<TEntity>, IRepository<TEntity> where TEntity : class
    {
        public Repository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            return await base.FindAsync(filter, orderBy, includeProperties);
        }

        public virtual async Task<TEntity> GetByID(object id)
        {
            return await dbSet.FindAsync(id);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAll()
        {
            return await dbSet.ToListAsync();
        }

        public virtual async Task Add(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException("invalid entity");

            await dbSet.AddAsync(entity);
        }

        public virtual void DeleteById(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
           dbSet.Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            dbContext.Entry(entityToUpdate).State = EntityState.Modified;
        }

    }
}
