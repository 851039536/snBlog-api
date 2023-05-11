using Snblog.IRepository;
using Snblog.IRepository.IRepository;
using Snblog.IService;

namespace Snblog.Service
{
    public class BaseService : IBaseService
    {
        private readonly IRepositoryFactory _repositoryFactory;
        private readonly IConcardContext _mydbcontext;
        public BaseService(IRepositoryFactory repositoryFactory, IConcardContext mydbcontext)
        {
            _repositoryFactory = repositoryFactory;
            _mydbcontext = mydbcontext;
        }

        public IRepositorys<T> CreateService<T>() where T : class, new()
        {
            return _repositoryFactory.CreateRepository<T>(_mydbcontext);
        }
    }
}
