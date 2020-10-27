using Snblog.IRepository;
using Snblog.IService;
using Snblog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
            int da = await Task.Run(() => CreateService<SnArticle>().AsyDelete(id));
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
        /// 按分类查询
        /// </summary>
        /// <param name="sortId"></param>
        /// <returns></returns>
        public List<SnArticle> GetTestWhere(int sortId)
        {
            var data = CreateService<SnArticle>().Where(s => s.SortId == sortId);
            return data.ToList();
        }


        /// <summary>
        /// 条件分页查询 - 支持排序
        /// </summary>
        /// <param name="label"></param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页记录条数</param>
        /// <param name="count">返回总条数</param>
        /// <param name="isDesc">是否倒序</param>
        public List<SnArticle> GetPagingWhere(int label, int pageIndex, int pageSize, out int count, bool isDesc)
        {
            IEnumerable<SnArticle> data;
            // if (label == 00)
            //{
            //    data = CreateService<SnArticle>().Wherepage(s => true, c => c.ArticleId, pageIndex, pageSize, out count, isDesc);
            //}
            //else
            //{
            //    data = CreateService<SnArticle>().Wherepage(s => s.LabelId == label, c => c.ArticleId, pageIndex, pageSize, out count, isDesc);
            //}
            data = label == 00 ? CreateService<SnArticle>().Wherepage(s => true, c => c.ArticleId, pageIndex, pageSize, out count, isDesc) : CreateService<SnArticle>().Wherepage(s => s.LabelId == label, c => c.ArticleId, pageIndex, pageSize, out count, isDesc);
            return data.ToList();

        }


        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="test"></param>
        /// <returns></returns>
        public async Task<SnArticle> AsyInsArticle(SnArticle test)
        {
            return await CreateService<SnArticle>().AysAdd(test);
        }

        public async Task<string> AysUpArticle(SnArticle test)
        {
            int da = await CreateService<SnArticle>().AysUpdate(test);
            // string data = da == 1 ? "更新成功" : "更新失败";
            string Func(int data) => data == 1 ? "更新成功" : "更新失败";
            return  Func(da);
        }

        public string DetTestId(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 返回总条数
        /// </summary>
        /// <returns></returns>
        public int GetArticleCount()
        {
            int data = CreateService<SnArticle>().Count();
            return data;
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

        /// <summary>
        /// 查询标签总数
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public int ConutLabel(int type)
        {
            return CreateService<SnArticle>().Count(c => c.LabelId == type);
        }
    }
}
