using System;
using System.Data;
using System.Linq;

namespace Logic.Utils
{
    public class UnitOfWork : IDisposable
    {
        private readonly ISession _context;
        private bool _isAlive = true;
        private bool _isCommitted;

        public UnitOfWork(ISession context)
        {
            _context = context;
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

        internal T Get<T>(long id)
            where T : class
        {
            return _context.Get<T>(id);
        }

        internal void SaveOrUpdate<T>(T entity)
        {
            _context.Attach(entity);
        }

        internal void Delete<T>(T entity)
        {
            _context.Delete(entity);
        }

        public IQueryable<T> Query<T>()
        {
            return _context.Query<T>();
        }
    }

    public interface ISession : IDisposable
    {
        void SaveChanges();
        T Get<T>(long id) where T : class;
        void Attach<T>(T entity);
        void Delete<T>(T entity);
        IQueryable<T> Query<T>();
    }
}
