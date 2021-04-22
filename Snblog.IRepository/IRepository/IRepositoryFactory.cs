using Snblog.IRepository.IRepository;

namespace Snblog.IRepository
{
    public interface IRepositoryFactory
    {
        IRepositorys<T> CreateRepository<T>(IconcardContext mydbcontext) where T : class;
    }
}
