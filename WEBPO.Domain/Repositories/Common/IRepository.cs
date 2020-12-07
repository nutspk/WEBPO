using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WEBPO.Domain.UnitOfWork.Collections;

namespace WEBPO.Domain.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");

        Task<TEntity> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "",
            bool disableTracking = true);

        Task<IPagedList<TEntity>> GetPagedListAsync(Expression<Func<TEntity, bool>> predicate = null,
                                                    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                                    string includeProperties = "",
                                                    int pageIndex = 0, int pageSize = 20, bool disableTracking = true,
                                                    CancellationToken cancellationToken = default(CancellationToken));

        Task<IPagedList<TResult>> GetPagedListAsync<TResult>(Expression<Func<TEntity, TResult>> selector,
                                                             Expression<Func<TEntity, bool>> predicate = null,
                                                             Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                                             string includeProperties = "",
                                                             int pageIndex = 0, int pageSize = 20, bool disableTracking = true,
                                                             CancellationToken cancellationToken = default(CancellationToken)) where TResult : class;

        int Count(Expression<Func<TEntity, bool>> predicate = null);

        Task<TEntity> GetByID(object id);
        Task<IEnumerable<TEntity>> GetAll();
        Task Add(TEntity entity);
        void Update(TEntity entity);
        void DeleteById(object id);
        void Delete(TEntity entity);
    }
}
