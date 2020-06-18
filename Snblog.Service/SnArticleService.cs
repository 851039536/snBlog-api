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
    public class SnArticleService : BaseService, ISnArticleService
    {
        public SnArticleService(IRepositoryFactory repositoryFactory, IconcardContext mydbcontext) : base(repositoryFactory, mydbcontext)
        {
        }

        public Task<string> AsyDetTestId(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<SnArticle>> AsyGetTest()
        {
            throw new NotImplementedException();
        }

        public async Task<SnArticle> AsyGetTestName(int id)
        {
           return await CreateService<SnArticle>().AysGetById(id);
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

        public List<SnArticle> GetTest()
        {
             var data = this.CreateService<SnArticle>();
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
