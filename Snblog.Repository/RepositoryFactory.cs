using Snblog.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Snblog.Repository
{
    public class RepositoryFactory : IRepositoryFactory
    {
        public IRepositorys<T> CreateRepository<T>(IconcardContext mydbcontext) where T : class
        {
            return new Repositorys<T>(mydbcontext);
        }
    }
}
