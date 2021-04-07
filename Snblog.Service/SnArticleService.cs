using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Serilog.Core;
using Snblog.Cache;
using Snblog.Cache.Cache;
using Snblog.Cache.CacheUtil;
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
        private readonly snblogContext _coreDbContext;//DB
        //创建内存缓存对象
        private static CacheManager _cache = new CacheManager();
        private static CacheUtil _cacheUtil = new CacheUtil();
        private readonly ILogger<SnArticleService> _logger; // <-添加此行
        private int result;
        private List<SnArticle> article = null;
        public SnArticleService(IRepositoryFactory repositoryFactory, IconcardContext mydbcontext, snblogContext coreDbContext, ILogger<SnArticleService> logger) : base(repositoryFactory, mydbcontext)
        {
            _coreDbContext = coreDbContext;
            _logger = logger ?? throw new ArgumentNullException(nameof(Logger));
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<string> AsyDetArticleId(int id)
        {
            int dataid = await Task.Run(() => CreateService<SnArticle>().AsyDelete(id));
            string data = dataid == 1 ? "删除成功" : "删除失败";
            return data;
        }

        public async Task<SnArticle> AsyGetTestName(int id)
        {
            SnArticle result = null;
            if (_cache.IsInCache("AsyGetTestName" + id)) //是否存在缓存
            {
                result = _cache.Get<SnArticle>("AsyGetTestName" + id);//读缓存值
            }
            else
            {
                _cache.Set_AbsoluteExpire("AsyGetTestName" + id, await CreateService<SnArticle>().AysGetById(id), _cache.time); //设置缓存
                result = _cache.Get<SnArticle>("AsyGetTestName" + id);
            }
            return result;
        }

        /// <summary>
        /// 分类ID查询 (缓存)
        /// </summary>
        /// <param name="sortId"></param>
        /// <returns></returns>
        public List<SnArticle> GetTestWhere(int sortId)
        {
            article = _cacheUtil.CacheString("GetTestWhere" + sortId, article);
            if (article == null)
            {
                article = CreateService<SnArticle>().Where(s => s.SortId == sortId).ToList();
                _cacheUtil.CacheString("GetTestWhere" + sortId, article);
                _logger.LogInformation("执行缓存: GetTestWhere" + sortId);
            }
            return article;
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
            // if (label == 00)
            //{
            //    data = CreateService<SnArticle>().Wherepage(s => true, c => c.ArticleId, pageIndex, pageSize, out count, isDesc);
            //}
            //else
            //{
            //    data = CreateService<SnArticle>().Wherepage(s => s.LabelId == label, c => c.ArticleId, pageIndex, pageSize, out count, isDesc);
            //}
            var result = label == 00 ? CreateService<SnArticle>().Wherepage(s => true, c => c.ArticleId, pageIndex, pageSize, out count, isDesc) : CreateService<SnArticle>().Wherepage(s => s.LabelId == label, c => c.ArticleId, pageIndex, pageSize, out count, isDesc);
            return result.ToList();
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
            int result = await CreateService<SnArticle>().AysUpdate(test);
            // string data = da == 1 ? "更新成功" : "更新失败";
            string Func(int data) => data == 1 ? "更新成功" : "更新失败";
            return Func(result);
        }


        /// <summary>
        /// 返回总条数
        /// </summary>
        /// <returns></returns>
        public int GetArticleCount()
        {

            result = _cacheUtil.CacheNumber("GetArticleCount", result);
            if (result == 0)
            {
                result = CreateService<SnArticle>().Count(); //获取数据值
                _cacheUtil.CacheNumber("GetArticleCount", result);
                _logger.LogInformation("执行缓存 GetArticleCount");
            }
            return result;
        }

        /// <summary>
        /// 查询标签总数
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public int ConutLabel(int type)
        {
            //读取缓存值
            result = _cacheUtil.CacheNumber("ConutLabel" + type, result);
            if (result == 0)
            {
                result = CreateService<SnArticle>().Count(c => c.LabelId == type);//获取数据值
                _cacheUtil.CacheNumber("ConutLabel" + type, result);//设置缓存值
                _logger.LogInformation("执行缓存: ConutLabel" + type);
            }
            return result;
        }

        public List<SnArticle> GetTest()
        {
            var result = this.CreateService<SnArticle>();
            return result.GetAll().ToList();
        }

        public async Task<int> CountAsync()
        {
            result = _cacheUtil.CacheNumber("CountSnArticle", result);
            if (result == 0)
            {
                  result = await _coreDbContext.SnArticle.CountAsync();
                _cacheUtil.CacheNumber("CountSnArticle", result);
            }
            return result;
        }

        public async Task<List<SnArticle>> GetAllAsync()
        {
            article = _cacheUtil.CacheString("GetAllSnArticle", article);
            if (article == null)
            {
                article = await _coreDbContext.SnArticle.ToListAsync();
                _cacheUtil.CacheString("GetAllSnArticle", article);
            }
            return article;
        }

        public async Task<int> GetSumAsync(string type)
        {
            int num = 0;
            switch (type) //按类型查询
            {
                case "read":
                    var read = await _coreDbContext.SnArticle.Select(c => c.Read).ToListAsync();
                    foreach (int item in read)
                    {
                        num += item;
                    }
                    break;
                case "text":
                    var text = await _coreDbContext.SnArticle.Select(c => c.Text).ToListAsync();
                    for (int i = 0; i < text.Count; i++)
                    {
                        num += text[i].Length;
                    }
                    break;
                case "give":
                    var give = await _coreDbContext.SnArticle.Select(c => c.Give).ToListAsync();
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

        public async Task<List<SnArticle>> GetFyTypeAsync(int type, int pageIndex, int pageSize, string name, bool isDesc)
        {
            if (isDesc) //降序
            {
                if (type.Equals(999))//表示查所有
                {
                    switch (name)
                    {
                        case "read":
                            return await _coreDbContext.SnArticle.Where(s => true)
                            .OrderByDescending(c => c.Read).Skip((pageIndex - 1) * pageSize)
                           .Take(pageSize).ToListAsync();
                        case "data":
                            return await _coreDbContext.SnArticle.Where(s => true)
                           .OrderByDescending(c => c.Time).Skip((pageIndex - 1) * pageSize)
                           .Take(pageSize).ToListAsync();
                        case "give":
                            return await _coreDbContext.SnArticle.Where(s => true)
                           .OrderByDescending(c => c.Give).Skip((pageIndex - 1) * pageSize)
                           .Take(pageSize).ToListAsync();
                        case "comment":
                            return await _coreDbContext.SnArticle.Where(s => true)
                           .OrderByDescending(c => c.Comment).Skip((pageIndex - 1) * pageSize)
                           .Take(pageSize).ToListAsync();
                        default:
                            return await _coreDbContext.SnArticle.Where(s => true)
                            .OrderByDescending(c => c.ArticleId).Skip((pageIndex - 1) * pageSize)
                           .Take(pageSize).ToListAsync();
                    }
                }
                else
                {
                    return await _coreDbContext.SnArticle.Where(s => s.SortId == type)
                  .OrderByDescending(c => c.ArticleId).Skip((pageIndex - 1) * pageSize)
                  .Take(pageSize).ToListAsync();
                }

            }
            else   //升序
            {
                if (type.Equals(999))//表示查所有
                {
                    switch (name)
                    {
                        case "read":
                            return await _coreDbContext.SnArticle.Where(s => true)
                          .OrderBy(c => c.Read).Skip((pageIndex - 1) * pageSize)
                          .Take(pageSize).ToListAsync();
                        case "data":
                            return await _coreDbContext.SnArticle.Where(s => true)
                          .OrderBy(c => c.Time).Skip((pageIndex - 1) * pageSize)
                          .Take(pageSize).ToListAsync();
                        case "give":
                            return await _coreDbContext.SnArticle.Where(s => true)
                          .OrderBy(c => c.Give).Skip((pageIndex - 1) * pageSize)
                          .Take(pageSize).ToListAsync();
                        case "comment":
                            return await _coreDbContext.SnArticle.Where(s => true)
                          .OrderBy(c => c.Comment).Skip((pageIndex - 1) * pageSize)
                          .Take(pageSize).ToListAsync();
                        default:
                            return await _coreDbContext.SnArticle.Where(s => true)
                            .OrderBy(c => c.ArticleId).Skip((pageIndex - 1) * pageSize)
                            .Take(pageSize).ToListAsync();
                    }
                }
                else
                {
                    return await _coreDbContext.SnArticle.Where(s => s.SortId == type)
                     .OrderBy(c => c.ArticleId).Skip((pageIndex - 1) * pageSize)
                      .Take(pageSize).ToListAsync();
                }

            }
        }
    }
}
