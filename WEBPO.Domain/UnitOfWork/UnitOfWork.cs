using WEBPO.Domain.Data;
using WEBPO.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore.Infrastructure;
using WEBPO.Domain.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Threading;

namespace WEBPO.Domain.UnitOfWork
{
    public class UnitOfWork<TContext> : IUnitOfWork<TContext> where TContext : DbContext, IDisposable
    {
        private Dictionary<(Type type, string name), object> _repositories;
        private readonly TContext _context;
        private bool disposed = false;
        private string UserUpdated;
        public UnitOfWork(TContext dbContext)
        {
            _context = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
        public TContext Context => _context;

        public void ActionBy(string userUpdated) {
            UserUpdated = userUpdated;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var changeEntries = _context.ChangeTracker.Entries().ToList();
            foreach (var entry in changeEntries)
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        if (entry.Entity is IBaseEntity) {
                            var track = entry.Entity as IBaseEntity;
                            track.IEntryDate = DateTime.Now;
                        }
                        break;
                    case EntityState.Modified:
                        if (entry.Entity is IBaseEntity)
                        {
                            var track = entry.Entity as IBaseEntity;
                            track.IUpdDate = DateTime.Now;
                            track.IUpdUserId = UserUpdated;
                        }
                        break;
                }
            }

            return await _context.SaveChangesAsync(cancellationToken);
        }

        public IRepository<TEntity> GetRepository<TEntity>(bool hasCustomRepository = false) where TEntity : class
        {
            if (hasCustomRepository)
            {
                var customRepo = _context.GetService<IRepository<TEntity>>();
                if (customRepo != null) return customRepo;
            }

            return (IRepository<TEntity>)GetOrAddRepository(typeof(TEntity), new Repository<TEntity>(Context as AppDbContext));
        }

        internal object GetOrAddRepository(Type type, object repo)
        {
            if (_repositories == null) _repositories = new Dictionary<(Type type, string Name), object>();

            if (_repositories.TryGetValue((type, repo.GetType().FullName), out var repository)) return repository;
            _repositories.Add((type, repo.GetType().FullName), repo);
            return repo;
        }

        [Obsolete]
        public int ExecuteSqlCommand(string sql, params object[] parameters) =>
            _context.Database.ExecuteSqlCommand(sql, parameters);
        public IQueryable<TEntity> FromSql<TEntity>(string sql, params object[] parameters) where TEntity : class
        {
            return _context.Set<TEntity>().FromSqlRaw(sql, parameters);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context?.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
