using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Serilog.Core;
using Snblog.Cache.Cache;
using Snblog.Cache.CacheUtil;
using Snblog.IRepository;
using Snblog.IService;
using Snblog.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Snblog.Service
{
    public class SnArticleService : BaseService, ISnArticleService
    {
        private readonly snblogContext _coreDbContext;//DB
        private readonly CacheUtil _cacheUtil;
        //创建内存缓存对象
        private readonly ILogger<SnArticleService> _logger; // <-添加此行
        private int result;
        private List<SnArticle> article = null;
        public SnArticleService(ICacheUtil cacheUtil, IRepositoryFactory repositoryFactory, IconcardContext mydbcontext, snblogContext coreDbContext, ILogger<SnArticleService> logger) : base(repositoryFactory, mydbcontext)
        {
            _coreDbContext = coreDbContext;
            _cacheUtil = (CacheUtil)cacheUtil;
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
            result = _cacheUtil.CacheString("AsyGetTestName" + id, result);
            if (result == null)
            {
                result = await _coreDbContext.SnArticle.FindAsync(id);
                _cacheUtil.CacheString("AsyGetTestName" + id, result);
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
                article = _coreDbContext.SnArticle.Where(s => s.label_id == sortId).ToList();
                _cacheUtil.CacheString("GetTestWhere" + sortId, article);
            }
            return article;
        }

        /// <summary>
        /// 条件分页查询 
        /// </summary>
        /// <param name="label"></param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页记录条数</param>
        /// <param name="count">返回总条数</param>
        /// <param name="isDesc">是否倒序</param>
        public async Task<List<SnArticle>> GetPagingWhereAsync(int label, int pageIndex, int pageSize, bool isDesc)
        {
            article = _cacheUtil.CacheString("GetPagingWhereAsync" + label + pageIndex + pageSize + isDesc, article);
            if (article == null)
            {
                article = await GetfyTest(label, pageIndex, pageSize, isDesc);
                _cacheUtil.CacheString("GetPagingWhereAsync" + label + pageIndex + pageSize + isDesc, article);
            }
            return article;
        }

        private async Task<List<SnArticle>> GetfyTest(int label, int pageIndex, int pageSize, bool isDesc)
        {
            if (label == 00)
            {
                if (isDesc)
                {
                    article = await _coreDbContext.SnArticle.OrderByDescending(c => c.article_id).Skip((pageIndex - 1) * pageSize)
                           .Take(pageSize).ToListAsync();
                }
                else
                {
                    article = await _coreDbContext.SnArticle.OrderBy(c => c.article_id).Skip((pageIndex - 1) * pageSize)
                             .Take(pageSize).ToListAsync();
                }
            }
            else
            {
                if (isDesc)
                {
                    article = await _coreDbContext.SnArticle.Where(s => s.label_id == label).OrderByDescending(c => c.article_id).Skip((pageIndex - 1) * pageSize)
                            .Take(pageSize).ToListAsync();
                }
                else
                {
                    article = await _coreDbContext.SnArticle.Where(s => s.label_id == label).OrderBy(c => c.article_id).Skip((pageIndex - 1) * pageSize)
                           .Take(pageSize).ToListAsync();
                }
            }
            return article;
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
                result = _coreDbContext.SnArticle.Where(c => c.label_id == type).Count();
                _cacheUtil.CacheNumber("ConutLabel" + type, result);//设置缓存值
            }
            return result;
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
            result = _cacheUtil.CacheNumber("GetSumAsync" + type, result);
            if (result == 0)
            {
                result = await GetSum(type);
                _cacheUtil.CacheNumber("GetSumAsync" + type, result);
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
                    var read = await _coreDbContext.SnArticle.Select(c => c.read).ToListAsync();
                    foreach (int item in read)
                    {
                        num += item;
                    }
                    break;
                case "text":
                    var text = await _coreDbContext.SnArticle.Select(c => c.text).ToListAsync();
                    for (int i = 0; i < text.Count; i++)
                    {
                        num += text[i].Length;
                    }
                    break;
                case "give":
                    var give = await _coreDbContext.SnArticle.Select(c => c.give).ToListAsync();
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
                            .OrderByDescending(c => c.read).Skip((pageIndex - 1) * pageSize)
                           .Take(pageSize).ToListAsync();
                        case "data":
                            return await _coreDbContext.SnArticle.Where(s => true)
                           .OrderByDescending(c => c.time).Skip((pageIndex - 1) * pageSize)
                           .Take(pageSize).ToListAsync();
                        case "give":
                            return await _coreDbContext.SnArticle.Where(s => true)
                           .OrderByDescending(c => c.give).Skip((pageIndex - 1) * pageSize)
                           .Take(pageSize).ToListAsync();
                        case "comment":
                            return await _coreDbContext.SnArticle.Where(s => true)
                           .OrderByDescending(c => c.comment).Skip((pageIndex - 1) * pageSize)
                           .Take(pageSize).ToListAsync();
                        default:
                            return await _coreDbContext.SnArticle.Where(s => true)
                            .OrderByDescending(c => c.article_id).Skip((pageIndex - 1) * pageSize)
                           .Take(pageSize).ToListAsync();
                    }
                }
                else
                {
                    return await _coreDbContext.SnArticle.Where(s => s.sort_id == type)
                  .OrderByDescending(c => c.article_id).Skip((pageIndex - 1) * pageSize)
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
                          .OrderBy(c => c.read).Skip((pageIndex - 1) * pageSize)
                          .Take(pageSize).ToListAsync();
                        case "data":
                            return await _coreDbContext.SnArticle.Where(s => true)
                          .OrderBy(c => c.time).Skip((pageIndex - 1) * pageSize)
                          .Take(pageSize).ToListAsync();
                        case "give":
                            return await _coreDbContext.SnArticle.Where(s => true)
                          .OrderBy(c => c.give).Skip((pageIndex - 1) * pageSize)
                          .Take(pageSize).ToListAsync();
                        case "comment":
                            return await _coreDbContext.SnArticle.Where(s => true)
                          .OrderBy(c => c.comment).Skip((pageIndex - 1) * pageSize)
                          .Take(pageSize).ToListAsync();
                        default:
                            return await _coreDbContext.SnArticle.Where(s => true)
                            .OrderBy(c => c.article_id).Skip((pageIndex - 1) * pageSize)
                            .Take(pageSize).ToListAsync();
                    }
                }
                else
                {
                    return await _coreDbContext.SnArticle.Where(s => s.sort_id == type)
                     .OrderBy(c => c.article_id).Skip((pageIndex - 1) * pageSize)
                      .Take(pageSize).ToListAsync();
                }

            }
        }
        public async Task<List<SnArticle>> GetFyTitleAsync(int pageIndex, int pageSize, bool isDesc)
        {
            article = _cacheUtil.CacheString("GetFyTitleAsync" + pageIndex + pageSize + isDesc, article); //设置缓存
            if (article == null)
            {
                article = await GetFyTitle(pageIndex, pageSize, isDesc); //读取数据
                _cacheUtil.CacheString("GetFyTitleAsync" + pageIndex + pageSize + isDesc, article); //设置缓存
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
            if (isDesc) //降序
            {
                var data = await _coreDbContext.SnArticle.Where(s => true).Select(s => new
                {
                    s.article_id,
                    s.title,
                    s.comment,
                    s.give,
                    s.read,
                    s.time,
                    s.title_text,
                    s.user_id
                }).OrderByDescending(c => c.article_id).Skip((pageIndex - 1) * pageSize)
                             .Take(pageSize).ToListAsync();

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
            else
            {
                var data = await _coreDbContext.SnArticle.Where(s => true).Select(s => new
                {
                    s.article_id,
                    s.title,
                    s.comment,
                    s.give,
                    s.read,
                    s.time,
                    s.title_text,
                    s.user_id
                }).OrderBy(c => c.article_id).Skip((pageIndex - 1) * pageSize)
                         .Take(pageSize).ToListAsync();
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
        }

        public async Task<bool> UpdatePortionAsync(SnArticle snArticle, string type)
        {
            var date = _coreDbContext.SnArticle.Update(snArticle);

            //默认不更新
            date.Property("user_id").IsModified = false;
            date.Property("title").IsModified = false;
            date.Property("title_text").IsModified = false;
            date.Property("text").IsModified = false;
            date.Property("time").IsModified = false;
            date.Property("label_id").IsModified = false;
            date.Property("give").IsModified = false;
            date.Property("read").IsModified = false;
            date.Property("comment").IsModified = false;
            date.Property("sort_id").IsModified = false;
            date.Property("type_title").IsModified = false;
            date.Property("url_img").IsModified = false;
            switch (type)
            {    //指定字段进行更新操作
                case "read":
                    date.Property(type).IsModified = true;
                    break;
                case "give":
                    date.Property(type).IsModified = true;
                    break;
                case "comment":
                    date.Property(type).IsModified = true;
                    break;
                default:
                    return false;
            }
            return await _coreDbContext.SaveChangesAsync() > 0;
        }

        public async Task<List<SnArticle>> GetTagtextAsync(int tag, bool isDesc)
        {
            article = _cacheUtil.CacheString("GetTagtextAsync" + tag + isDesc, article); //设置缓存
            if (article == null)
            {
                article = await GetTagtext(tag, isDesc); //读取数据
                _cacheUtil.CacheString("GetTagtextAsync" + tag + isDesc, article); //设置缓存
            }
            return article;
        }

        private async Task<List<SnArticle>> GetTagtext(int tag, bool isDesc)
        {
            if (isDesc)
            {
                var data = await _coreDbContext.SnArticle.Where(s => s.label_id == tag).Select(s => new
                {
                    s.article_id,
                    s.title,
                    s.title_text,
                    s.time,
                    s.give,
                    s.read
                }).OrderByDescending(s => s.article_id).ToListAsync();
                var list = new List<SnArticle>();
                foreach (var t in data)
                {
                    var s = new SnArticle();
                    s.article_id = t.article_id;
                    s.title = t.title;
                    s.title_text = t.title_text;
                    s.time = t.time;
                    s.give = t.give;
                    s.read = t.read;
                    list.Add(s);
                }
                return list;
            }
            else
            {
                var data = await _coreDbContext.SnArticle.Where(s => s.label_id == tag).Select(s => new
                {
                    s.article_id,
                    s.title,
                    s.title_text,
                    s.time,
                    s.give,
                    s.read
                }).OrderBy(s => s.article_id).ToListAsync();
                var list = new List<SnArticle>();
                foreach (var t in data)
                {
                    var s = new SnArticle();
                    s.article_id = t.article_id;
                    s.title = t.title;
                    s.title_text = t.title_text;
                    s.time = t.time;
                    s.give = t.give;
                    s.read = t.read;
                    list.Add(s);
                }
                return list;
            }
        }

        public List<SnArticle> GetTest()
        {
            throw new NotImplementedException();
        }
    }
}
