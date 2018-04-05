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

        public T GetById(long id)
        {
            return _unitOfWork.Get<T>(id);
        }

        public void Add(T entity)
        {
            _unitOfWork.SaveOrUpdate(entity);
        }
    }
}
