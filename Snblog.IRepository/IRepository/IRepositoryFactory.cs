using Snblog.IRepository.IRepository;

namespace Snblog.IRepository
{
    public interface IRepositoryFactory
    {
        IRepositorys<T> CreateRepository<T>(IConcardContext mydbcontext) where T : class;
    }
}
