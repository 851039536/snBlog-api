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
    public class SnNavigationService : BaseService, ISnNavigationService
    {
        public SnNavigationService(IRepositoryFactory repositoryFactory, IconcardContext mydbcontext) : base(repositoryFactory, mydbcontext)
        {
        }

        /// <summary>
        /// 条件分页查询 - 支持排序
        /// </summary>
        /// <typeparam name="TOrder">排序约束</typeparam>
        /// <param name="where">过滤条件</param>
        /// <param name="order">排序条件</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页记录条数</param>
        /// <param name="count">返回总条数</param>
        /// <param name="isDesc">是否倒序</param>
        public List<SnNavigation> GetPagingWhere(string type, int pageIndex, int pageSize, out int count, bool isDesc)
        {
            IEnumerable<SnNavigation> data;
            if (type == "all")
            {
                data = CreateService<SnNavigation>().Wherepage(s => s.NavType != null, c => c.NavId, pageIndex, pageSize, out count, isDesc);
            }
            else
            {
                data = CreateService<SnNavigation>().Wherepage(s => s.NavType == type, c => c.NavId, pageIndex, pageSize, out count, isDesc);
            }

            return data.ToList();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<string> AsyDelNavigation(int id)
        {
            int da = await Task.Run(() => CreateService<SnNavigation>().AsyDelete(id));
            string data = da == 1 ? "删除成功" : "删除失败";
            return data;
        }

        public Task<List<SnNavigation>> AsyGetTest()
        {
            throw new NotImplementedException();
            //var data = CreateService<SnNavigation>()
        }

        public Task<SnArticle> AsyGetTestName(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<SnNavigation>> AsyGetWhereTest(string type, bool fag)
        {
            var data = this.CreateService<SnNavigation>().Where(c => c.NavType == type, s => s.NavId, fag);
            return await data.ToListAsync();
        }

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="test"></param>
        /// <returns></returns>
        public async Task<SnNavigation> AsyIntNavigation(SnNavigation test)
        {
            return await Task.Run(() => CreateService<SnNavigation>().AysAdd(test));
        }

        public async Task<string> AysUpNavigation(SnNavigation test)
        {
            int da = await CreateService<SnNavigation>().AysUpdate(test);
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
            return data;
        }
        /// <summary>
        /// 查询总数
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public int GetNavigationCount(string type)
        {
            return CreateService<SnNavigation>().Count(c => c.NavType == type);
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

        public List<SnNavigation> GetDistTest(string type)
        {
            var data = CreateService<SnNavigation>().Distinct(s => s.NavType == type);

            return data.ToList();
        }

        public SnNavigation GetNavigationId(int id)
        {
            return CreateService<SnNavigation>().GetById(id);
        }
    }
}
