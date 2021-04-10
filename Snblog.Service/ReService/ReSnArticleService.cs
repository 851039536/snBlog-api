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
        private static CacheUtil _cacheUtil = new CacheUtil();
        private int result;
        private List<SnArticle> article = null;
        private IQueryable<SnArticle> IQarticle = null;
        public ReSnArticleService(IRepositoryFactory repositoryFactory, IconcardContext mydbcontext) : base(repositoryFactory, mydbcontext)
        {
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

        public async Task<List<SnArticle>> GetLabelAllAsync(int id)
        {
            article = _cacheUtil.CacheString("GetLabelAllAsync" + id, article);
            if (article == null)
            {
                article= await CreateService<SnArticle>().Where(s => s.label_id == id).ToListAsync();
                _cacheUtil.CacheString("GetLabelAllAsync" + id, article);
            }
            return article;
        }
    }
}
