using Microsoft.EntityFrameworkCore;
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


        public List<SnOne> GetOne()
        {
            var data = this.CreateService<SnOne>();
            return data.GetAll().ToList();
        }
        public async Task<List<SnOne>> AsyGetOne()
        {
            var data = CreateService<SnOne>();
            return await data.GetAll().ToListAsync();
        }

        public int OneCount()
        {
           int data = CreateService<SnOne>().Count();
            return  data;
        }

        public Task<string> AsyDetArticleId(int id)
        {
            throw new NotImplementedException();
        }



        public async Task<SnOne> AsyGetOneId(int id)
        {
             return await CreateService<SnOne>().AysGetById(id);
        }

        public Task<SnArticle> AsyInsArticle(SnArticle test)
        {
            throw new NotImplementedException();
        }

        public Task<string> AysUpArticle(SnArticle test)
        {
            throw new NotImplementedException();
        }

        public int OneCountType(string type)
        {
           return CreateService<SnOne>().Count(c => c.OneAuthor == type);
        }

        public string DetTestId(int id)
        {
            throw new NotImplementedException();
        }

     

        public List<SnArticle> GetPagingWhere(int pageIndex, int pageSize, out int count, bool isDesc)
        {
            throw new NotImplementedException();
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
