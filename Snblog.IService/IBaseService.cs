using Snblog.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Snblog.IService
{
   public interface IBaseService
    {
        IRepositorys<T> CreateService<T>() where T : class, new();
    }
}
