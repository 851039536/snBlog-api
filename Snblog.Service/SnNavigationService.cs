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

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<string> AsyDelNavigation(int id)
        {
           int da= await Task.Run(() => CreateService<SnNavigation>().AsyDelete(id));
           string data = da == 1 ? "删除成功" : "删除失败";
           return data;
        }

        public Task<List<SnNavigation>> AsyGetTest()
        {
            throw new NotImplementedException();
        }

        public Task<SnArticle> AsyGetTestName(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="test"></param>
        /// <returns></returns>
        public async Task<SnNavigation> AsyIntNavigation(SnNavigation test)
        {
             return await Task.Run(()=> CreateService<SnNavigation>().AysAdd(test));
        }

        public async Task<string> AysUpNavigation(SnNavigation test)
        {
            int da= await Task.Run(()=> CreateService<SnNavigation>().AysUpdate(test));
            string data = da == 1 ? "更新成功" : "更新失败";
            return data;
        }

        public Task<string> AysUpTest(SnNavigation test)
        {
            throw new NotImplementedException();
        }

        public string DetTestId(int id)
        {
            throw new NotImplementedException();
        }

        public int GetNavigationCount()
        {
           int data = CreateService<SnNavigation>().Count();
          return  data;
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

        public SnArticle IntTest(SnNavigation test)
        {
            throw new NotImplementedException();
        }

        public string UpTest(SnArticle test)
        {
            throw new NotImplementedException();
        }

        public string UpTest(SnNavigation test)
        {
            throw new NotImplementedException();
        }

        Task<SnNavigation> ISnNavigationService.AsyGetTestName(int id)
        {
            throw new NotImplementedException();
        }
    }
}
