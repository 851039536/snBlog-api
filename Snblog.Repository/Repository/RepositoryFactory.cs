namespace Snblog.Repository.Repository
{
    public class RepositoryFactory : IRepositoryFactory
    {
        public IRepositorys<T> CreateRepository<T>(IConcardContext mydbcontext) where T : class
        {
            return new Repositorys<T>(mydbcontext);
        }
    }
}
