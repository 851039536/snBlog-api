using System;
using System.Collections.Generic;
using System.Text;

namespace Snblog.IRepository
{
   public interface IRepositoryFactory
    {
        IRepositorys<T> CreateRepository<T>(IconcardContext mydbcontext) where T : class;
    }
}
