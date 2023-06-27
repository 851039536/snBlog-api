namespace Snblog.Repository.Repository
{
    public class RepositoryFactory : IRepositoryFactory
    {
        public IRepositorys<T> CreateRepository<T>(IConcardContext myDbContext) where T : class
        {
            return new Repositorys<T>(myDbContext);
        }
    }
}
