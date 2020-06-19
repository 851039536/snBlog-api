using Snblog.IRepository;
using Snblog.IService;
using Snblog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snblog.Service
{
    public class SnNavigationService : BaseService, ISnNavigationService
    {
        public SnNavigationService(IRepositoryFactory repositoryFactory, IconcardContext mydbcontext) : base(repositoryFactory, mydbcontext)
        {
        }

        public Task<string> AsyDetTestId(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<SnNavigation>> AsyGetTest()
        {
            throw new NotImplementedException();
        }

        public Task<SnArticle> AsyGetTestName(int id)
        {
            throw new NotImplementedException();
        }

        public Task<SnArticle> AsyIntTest(SnArticle test)
        {
            throw new NotImplementedException();
        }

        public Task<string> AysUpTest(SnArticle test)
        {
            throw new NotImplementedException();
        }

        public string DetTestId(int id)
        {
            throw new NotImplementedException();
        }

        public List<SnNavigation> GetSnNavigation()
        {
          var data = this.CreateService<SnNavigation>();
           return data.GetAll().ToList();
        }

        public SnArticle IntTest(SnArticle test)
        {
            throw new NotImplementedException();
        }

        public string UpTest(SnArticle test)
        {
            throw new NotImplementedException();
        }
    }
}
