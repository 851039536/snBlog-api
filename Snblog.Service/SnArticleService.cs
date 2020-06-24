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

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<string> AsyDetArticleId(int id)
        {
           int da= await Task.Run(() => CreateService<SnArticle>().AsyDelete(id));
           string data = da == 1 ? "删除成功" : "删除失败";
           return data;
        }

        public Task<List<SnArticle>> AsyGetTest()
        {
            throw new NotImplementedException();
        }

        public async Task<SnArticle> AsyGetTestName(int id)
        {
           return await CreateService<SnArticle>().AysGetById(id);
        }

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="test"></param>
        /// <returns></returns>
        public async Task<SnArticle> AsyInsArticle(SnArticle test)
        {
             return await Task.Run(()=> CreateService<SnArticle>().AysAdd(test));
        }

        public async Task<string> AysUpArticle(SnArticle test)
        {
             //int da=  CreateService<typecho_test>().Update(test);
            int da= await Task.Run(()=> CreateService<SnArticle>().AysUpdate(test));
            string data = da == 1 ? "更新成功" : "更新失败";
            return data;
        }

        public string DetTestId(int id)
        {
            throw new NotImplementedException();
        }

        public int GetArticleCount()
        {
           int data = CreateService<SnArticle>().Count();
          return  data;
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
