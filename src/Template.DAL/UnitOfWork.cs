using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Template.DAL.EfContext;
using Template.Infrastructure;

namespace Template.DAL
{
    public sealed class UnitOfWork : IDisposable, IUnitOfWork
    {
        private readonly ApplicationContext _context;
        private readonly ILogger<UnitOfWork> _logger;
        private bool _isAlive = true;
        private bool _isCommitted;

        public UnitOfWork(ApplicationContext context, ILogger<UnitOfWork> logger)
        {
            _context = context;
            _logger = logger;
        }

        public void Dispose()
        {
            if (!_isAlive)
                return;

            _isAlive = false;

            try
            {
                if (_isCommitted)
                {
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "SaveChanges Error");
            }
            finally
            {
                _context.Dispose();
            }
        }

        public void Commit()
        {
            if (!_isAlive)
                return;

            _isCommitted = true;
        }

        public async Task<T> Get<T>(long id, CancellationToken cancellationToken) where T : Entity
        {
            cancellationToken.ThrowIfCancellationRequested();

            var dbSet = _context.Set<T>();
            return await dbSet.FindAsync(id);
        }

        public void Insert<T>(T entity) where T : Entity
        {
            _context.Set<T>().Attach(entity);
        }

        public void Delete<T>(T entity) where T : Entity
        {
            var dbSet = _context.Set<T>();
            dbSet.Remove(entity);
        }

        public IQueryable<T> Query<T>() where T : Entity
        {
            return _context.Set<T>().AsQueryable();
        }
    }
}
