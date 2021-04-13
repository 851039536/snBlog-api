using Microsoft.EntityFrameworkCore;
using Snblog.Cache.CacheUtil;
using Snblog.IRepository;
using Snblog.IService.IReService;
using Snblog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snblog.Service.ReService
{
    public class ReSnArticleService : BaseService, IReSnArticleService
    {
        private readonly CacheUtil _cacheUtil;
        private int result;
        private List<SnArticle> article = null;

        public ReSnArticleService(ICacheUtil cacheUtil, IRepositoryFactory repositoryFactory, IconcardContext mydbcontext) : base(repositoryFactory, mydbcontext)
        {
            _cacheUtil = (CacheUtil)cacheUtil;
        }

        public async Task<int> CountAsync()
        {
            result = _cacheUtil.CacheNumber("CountAsync", result);
            if (result == 0)
            {
                result = CreateService<SnArticle>().Count();
                _cacheUtil.CacheNumber("CountAsync", result);
            }
            return result;
        }

        public int CountAsync(int type)
        {
            //读取缓存值
            result = _cacheUtil.CacheNumber("CountAsync" + type, result);
            if (result == 0)
            {
                result = CreateService<SnArticle>().Count(c => c.label_id == type);//获取数据值
                _cacheUtil.CacheNumber("CountAsync" + type, result);//设置缓存值
            }
            return result;
        }

        public async Task<List<SnArticle>> GetAllAsync()
        {
            article = _cacheUtil.CacheString("GetAllSnArticleAsync", article);
            if (article == null)
            {
                article = await CreateService<SnArticle>().GetAllAsync();
                _cacheUtil.CacheString("GetAllSnArticleAsync", article);
            }
            return article;
        }

        public async Task<SnArticle> GetByIdAsync(int id)
        {
            SnArticle result = null;
            result = _cacheUtil.CacheString("GetByIdAsync" + id, result);
            if (result == null)
            {
                result = await CreateService<SnArticle>().GetByIdAsync(id);
                _cacheUtil.CacheString("GetByIdAsync" + id, result);
            }
            return result;
        }

        public async Task<List<SnArticle>> GetFyTitleAsync(int pageIndex, int pageSize, bool isDesc)
        {
            article = _cacheUtil.CacheString("ReGetFyTitleAsync" + pageIndex + pageSize + isDesc, article); //设置缓存
            if (article == null)
            {
                article = await GetFyTitle(pageIndex, pageSize, isDesc); //读取数据
                _cacheUtil.CacheString("ReGetFyTitleAsync" + pageIndex + pageSize + isDesc, article); //设置缓存
            }
            return article;
        }

        /// <summary>
        /// 读取分页数据
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        private async Task<List<SnArticle>> GetFyTitle(int pageIndex, int pageSize, bool isDesc)
        {
            var data = await Task.Run(() => CreateService<SnArticle>().Wherepage(s => true, c => c.article_id, pageIndex, pageSize, out int count, isDesc).Select(s => new
            {
                s.article_id,
                s.title,
                s.comment,
                s.give,
                s.read,
                s.time,
                s.title_text,
                s.user_id
            }).ToList());
            //解决方案二：foreach遍历
            var list = new List<SnArticle>();
            foreach (var t in data)
            {
                var s = new SnArticle();
                s.article_id = t.article_id;
                s.title = t.title;
                s.comment = t.comment;
                s.give = t.give;
                s.read = t.read;
                s.time = t.time;
                s.title_text = t.title_text;
                s.user_id = t.user_id;
                list.Add(s);
            }
            return list;
        }

        public async Task<List<SnArticle>> GetLabelAllAsync(int id)
        {
            article = _cacheUtil.CacheString("GetLabelAllAsync" + id, article);
            if (article == null)
            {
                article = await CreateService<SnArticle>().Where(s => s.label_id == id).ToListAsync();
                _cacheUtil.CacheString("GetLabelAllAsync" + id, article);
            }
            return article;
        }

        public async Task<int> GetSumAsync(string type)
        {
            result = _cacheUtil.CacheNumber("ReGetSumAsync" + type, result);
            if (result == 0)
            {
                result = await GetSum(type);
                _cacheUtil.CacheNumber("ReGetSumAsync" + type, result);
            }
            return result;
        }

        /// <summary>
        /// 读取总字数
        /// </summary>
        /// <param name="type">阅读/点赞/评论</param>
        /// <returns></returns>
        private async Task<int> GetSum(string type)
        {
            int num = 0;
            switch (type) //按类型查询
            {
                case "read":

                    var read = await CreateService<SnArticle>().Where(s => true).Select(c => c.read).ToListAsync();
                    foreach (int item in read)
                    {
                        num += item;
                    }
                    break;
                case "text":
                    var text = await CreateService<SnArticle>().Where(s => true).Select(c => c.text).ToListAsync();
                    for (int i = 0; i < text.Count; i++)
                    {
                        num += text[i].Length;
                    }
                    break;
                case "give":
                    var give = await CreateService<SnArticle>().Where(s => true).Select(c => c.give).ToListAsync();
                    foreach (int item in give)
                    {
                        num += item;
                    }
                    break;
                default:
                    break;
            }

            return num;
        }

        public async Task< List<SnArticle>> GetTypeFyTextAsync(int type, int pageIndex, int pageSize, bool isDesc)
        {
            article = _cacheUtil.CacheString("ReGetTypeFyTextAsync" + type + pageIndex + isDesc, article);
            if (article == null)
            {
                article = await GetTypeFy(type, pageIndex, pageSize, isDesc);
                _cacheUtil.CacheString("ReGetTypeFyTextAsync" + type + pageIndex + isDesc, article);
            }
            return article;

        }

        private async Task<List<SnArticle>> GetTypeFy(int type, int pageIndex, int pageSize, bool isDesc)
        {
            if (type == 00)
            {
                var data = await CreateService<SnArticle>().WherepageAsync(s => true, c => c.article_id, pageIndex, pageSize, isDesc);
                return data.ToList();
            }
            else
            {
                var data = await CreateService<SnArticle>().WherepageAsync(s => s.label_id == type, c => c.article_id, pageIndex, pageSize, isDesc);
                return data.ToList();
            }
        }
    }
}
