using Snblog.IRepository;
using Snblog.IRepository.IRepository;

namespace Snblog.Repository.Repository
{
    public class RepositoryFactory : IRepositoryFactory
    {
        public IRepositorys<T> CreateRepository<T>(IconcardContext mydbcontext) where T : class
        {
            return new Repositorys<T>(mydbcontext);
        }
    }
}
