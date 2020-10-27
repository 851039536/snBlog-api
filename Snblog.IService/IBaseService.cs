using Snblog.IRepository;

namespace Snblog.IService
{
    public interface IBaseService
    {
        IRepositorys<T> CreateService<T>() where T : class, new();
    }
}
