using WEBPO.Domain.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
    public class BaseRepository<TEntity> where TEntity : class
    {
        internal DbSet<TEntity> dbSet;
        internal AppDbContext dbContext;
        public BaseRepository(AppDbContext context)
        {
            this.dbContext = context ?? throw new ArgumentException(nameof(context));
            this.dbSet = context.Set<TEntity>();
        }

        public virtual Task<IPagedList<TEntity>> GetPagedListAsync(Expression<Func<TEntity, bool>> predicate = null,
                                                           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                                           string includeProperties = "", int pageIndex = 0, int pageSize = 20,
                                                           bool disableTracking = true,
                                                           CancellationToken cancellationToken = default(CancellationToken))
        {
            IQueryable<TEntity> query = dbSet;

            if (disableTracking) query = query.AsNoTracking();


            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (predicate != null) query = query.Where(predicate);

            if (orderBy != null)
                return orderBy(query).ToPagedListAsync(pageIndex, pageSize, 0, cancellationToken);
            else
                return query.ToPagedListAsync(pageIndex, pageSize, 0, cancellationToken);
        }

        public virtual Task<IPagedList<TResult>> GetPagedListAsync<TResult>(Expression<Func<TEntity, TResult>> selector,
                                                                    Expression<Func<TEntity, bool>> predicate = null,
                                                                    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                                                    string includeProperties = "", int pageIndex = 0, int pageSize = 20,
                                                                    bool disableTracking = true,
                                                                    CancellationToken cancellationToken = default(CancellationToken))
            where TResult : class
        {
            IQueryable<TEntity> query = dbSet;
            if (disableTracking) query = query.AsNoTracking();

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (predicate != null) query = query.Where(predicate);


            if (orderBy != null)
                return orderBy(query).Select(selector).ToPagedListAsync(pageIndex, pageSize, 0, cancellationToken);
            else
                return query.Select(selector).ToPagedListAsync(pageIndex, pageSize, 0, cancellationToken);
        }


        public virtual async Task<TEntity> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate = null,
                                                  Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                                  string includeProperties = "",
                                                  bool disableTracking = true)
        {
            IQueryable<TEntity> query = dbSet;

            if (disableTracking) query = query.AsNoTracking();

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (orderBy != null)
            {
                return await orderBy(query).FirstOrDefaultAsync();
            }
            else
            {
                return await query.FirstOrDefaultAsync();
            }
        }


        public virtual async Task<IEnumerable<TEntity>> FindAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null) query = query.Where(filter);

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
                return await orderBy(query).AsNoTracking().ToListAsync();
            else
                return await query.AsNoTracking().ToListAsync();
        }

        public IQueryable GetList(Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "", int index = 0, int size = 20)
        {
            IQueryable<TEntity> query = dbSet;
            query = query.AsNoTracking();

            foreach (var includeProperty in includeProperties.Split
                 (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (predicate != null) query = query.Where(predicate);

            return orderBy != null ? orderBy(query).Skip(size * (index - 1)).Take(size) : query.Skip(size * (index - 1)).Take(size);
        }


        public IQueryable<TResult> GetList<TResult>(Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "", int index = 0, int size = 20) where TResult : class
        {
            IQueryable<TEntity> query = dbSet;
            query = query.AsNoTracking();

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (predicate != null) query = query.Where(predicate);

            return orderBy != null
                ? orderBy(query).Select(selector).Skip(size * (index - 1)).Take(size)
                : query.Select(selector).Skip(size * (index - 1)).Take(size);
        }

        public virtual int Count(Expression<Func<TEntity, bool>> predicate = null)
        {
            if (predicate == null)
            {
                return dbSet.Count();
            }
            else
            {
                return dbSet.Count(predicate);
            }
        }


    }
}
