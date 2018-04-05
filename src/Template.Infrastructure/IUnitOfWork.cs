using System.Linq;

namespace Logic.Common
{
    public interface IUnitOfWork
    {
        void Commit();

        T Get<T>(long id) where T : class;

        void SaveOrUpdate<T>(T entity);

        void Delete<T>(T entity);

        IQueryable<T> Query<T>();
    }
}