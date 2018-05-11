using System.Threading;
using System.Threading.Tasks;

namespace Template.Infrastructure
{
    public abstract class Repository<T>
        where T : Entity
    {
        protected readonly IUnitOfWork _unitOfWork;

        protected Repository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<T> GetById(long id, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await 
                _unitOfWork.Get<T>(id, cancellationToken);
        }

        public void Add(T entity)
        {
            _unitOfWork.Insert(entity);
        }
    }
}
