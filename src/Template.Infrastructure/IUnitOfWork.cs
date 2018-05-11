namespace Template.Infrastructure
{
    public interface IUnitOfWork : IRepository
    {
        void Commit();
    }
}