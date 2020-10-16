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
    public class SnOneService : BaseService, ISnOneService
    {
        public SnOneService(IRepositoryFactory repositoryFactory, IconcardContext mydbcontext) : base(repositoryFactory, mydbcontext)
        {
        }

        public Task<string> AsyDetArticleId(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<SnOne>> AsyGetTest()
        {
            throw new NotImplementedException();
        }

        public Task<SnOne> AsyGetTestName(int id)
        {
            throw new NotImplementedException();
        }

        public Task<SnArticle> AsyInsArticle(SnArticle test)
        {
            throw new NotImplementedException();
        }

        public Task<string> AysUpArticle(SnArticle test)
        {
            throw new NotImplementedException();
        }

        public int ConutLabel(int type)
        {
            throw new NotImplementedException();
        }

        public string DetTestId(int id)
        {
            throw new NotImplementedException();
        }

        public int GetArticleCount()
        {
            throw new NotImplementedException();
        }

        public List<SnArticle> GetPagingWhere(int pageIndex, int pageSize, out int count, bool isDesc)
        {
            throw new NotImplementedException();
        }

        public List<SnOne> GetTest()
        {
            var data = this.CreateService<SnOne>();
            return data.GetAll().ToList();
        }

        public List<SnOne> GetTestWhere(int id)
        {
            throw new NotImplementedException();
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
