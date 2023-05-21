using Snblog.IRepository.IRepository;

namespace Snblog.Service
{
    public class BaseService : IBaseService
    {
        private readonly IRepositoryFactory _repositoryFactory;
        private readonly IConcardContext _myDbcontext;

        protected BaseService(IRepositoryFactory repositoryFactory, IConcardContext myDbcontext)
        {
            _repositoryFactory = repositoryFactory;
            _myDbcontext = myDbcontext;
        }

        public IRepositorys<T> CreateService<T>() where T : class, new()
        {
            return _repositoryFactory.CreateRepository<T>(_myDbcontext);
        }
    }
}
