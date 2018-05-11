using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Template.Infrastructure
{
    public interface IRepository
    {
        Task<T> Get<T>(long id, CancellationToken cancellationToken) where T : Entity;

        void Insert<T>(T entity) where T : Entity;

        void Delete<T>(T entity) where T : Entity;

        IQueryable<T> Query<T>() where T : Entity;
    }
}