using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Template.Infrastructure
{
    public interface IUnitOfWork : IRepository
    {
        void Commit();
    }
    public interface IRepository
    {
        Task<T> Get<T>(long id, CancellationToken cancellationToken) where T : class;
        void SaveOrUpdate<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        IQueryable<T> Query<T>() where T : class;
    }
}