using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Serilog.Core;
using Snblog.Cache.CacheUtil;
using Snblog.IService;
using Snblog.Models;

namespace Snblog.Service.Service
{
    public class SnArticleService : ISnArticleService
    {
        private readonly snblogContext _service;//DB
        private readonly CacheUtil _cacheutil;
        //创建内存缓存对象
        private readonly ILogger<SnArticleService> _logger;
        private int result_Int;
        private List<SnArticle> result_List = null;
        public SnArticleService(ICacheUtil cacheUtil, snblogContext coreDbContext, ILogger<SnArticleService> logger)
        {
            _service = coreDbContext;
            _cacheutil = (CacheUtil)cacheUtil;
            _logger = logger ?? throw new ArgumentNullException(nameof(Logger));
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(int id)
        {
            var todoItem = await _service.SnArticle.FindAsync(id);
            if (todoItem == null) return false;
            _service.SnArticle.Remove(todoItem);
            return await _service.SaveChangesAsync() > 0;
        }

        public async Task<SnArticle> AsyGetTestName(int id)
        {
            SnArticle result = null;
            result = _cacheutil.CacheString("AsyGetTestName" + id, result);
            if (result == null)
            {
                result = await _service.SnArticle.FindAsync(id);
                _cacheutil.CacheString("AsyGetTestName" + id, result);
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
            result_List = _cacheutil.CacheString("GetTestWhere" + sortId, result_List);
            if (result_List == null)
            {
                result_List = _service.SnArticle.Where(s => s.label_id == sortId).ToList();
                _cacheutil.CacheString("GetTestWhere" + sortId, result_List);
            }
            return result_List;
        }

        /// <summary>
        /// 条件分页查询 
        /// </summary>
        /// <param name="label"></param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页记录条数</param>
        /// <param name="isDesc">是否倒序</param>
        public async Task<List<SnArticle>> GetPagingWhereAsync(int label, int pageIndex, int pageSize, bool isDesc)
        {
            result_List = _cacheutil.CacheString("GetPagingWhereAsync" + label + pageIndex + pageSize + isDesc, result_List);
            if (result_List == null)
            {
                result_List = await GetfyTest(label, pageIndex, pageSize, isDesc);
                _cacheutil.CacheString("GetPagingWhereAsync" + label + pageIndex + pageSize + isDesc, result_List);
            }
            return result_List;
        }

        private async Task<List<SnArticle>> GetfyTest(int label, int pageIndex, int pageSize, bool isDesc)
        {
            if (label == 00)
            {
                if (isDesc)
                {
                    result_List = await _service.SnArticle.OrderByDescending(c => c.article_id).Skip((pageIndex - 1) * pageSize)
                           .Take(pageSize).ToListAsync();
                }
                else
                {
                    result_List = await _service.SnArticle.OrderBy(c => c.article_id).Skip((pageIndex - 1) * pageSize)
                             .Take(pageSize).ToListAsync();
                }
            }
            else
            {
                if (isDesc)
                {
                    result_List = await _service.SnArticle.Where(s => s.label_id == label).OrderByDescending(c => c.article_id).Skip((pageIndex - 1) * pageSize)
                            .Take(pageSize).ToListAsync();
                }
                else
                {
                    result_List = await _service.SnArticle.Where(s => s.label_id == label).OrderBy(c => c.article_id).Skip((pageIndex - 1) * pageSize)
                           .Take(pageSize).ToListAsync();
                }
            }
            return result_List;
        }


       /// <summary>
       /// 添加数据
       /// </summary>
       /// <param name="entity"></param>
       /// <returns></returns>
        public async Task<bool> AddAsync(SnArticle entity)
        {
            await _service.SnArticle.AddAsync(entity);
            return await _service.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(SnArticle entity)
        {
            _service.SnArticle.Update(entity);
            return await _service.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// 查询标签总数
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public int ConutLabel(int type)
        {
            //读取缓存值
            result_Int = _cacheutil.CacheNumber("ConutLabel" + type, result_Int);
            if (result_Int == 0)
            {
                result_Int = _service.SnArticle.Where(c => c.label_id == type).Count();
                _cacheutil.CacheNumber("ConutLabel" + type, result_Int);//设置缓存值
            }
            return result_Int;
        }


        public async Task<int> CountAsync()
        {
            result_Int = _cacheutil.CacheNumber("CountSnArticle", result_Int);
            if (result_Int == 0)
            {
                result_Int = await _service.SnArticle.CountAsync();
                _cacheutil.CacheNumber("CountSnArticle", result_Int);
            }
            return result_Int;
        }

        public async Task<List<SnArticle>> GetAllAsync()
        {
            result_List = _cacheutil.CacheString("GetAllSnArticle", result_List);
            if (result_List == null)
            {
                result_List = await _service.SnArticle.ToListAsync();
                _cacheutil.CacheString("GetAllSnArticle", result_List);
            }
            return result_List;
        }

        public async Task<int> GetSumAsync(string type)
        {
            result_Int = _cacheutil.CacheNumber("GetSumAsync" + type, result_Int);
            if (result_Int == 0)
            {
                result_Int = await GetSum(type);
                _cacheutil.CacheNumber("GetSumAsync" + type, result_Int);
            }
            return result_Int;
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
                    var read = await _service.SnArticle.Select(c => c.read).ToListAsync();
                    foreach (int item in read)
                    {
                        num += item;
                    }
                    break;
                case "text":
                    var text = await _service.SnArticle.Select(c => c.text).ToListAsync();
                    for (int i = 0; i < text.Count; i++)
                    {
                        num += text[i].Length;
                    }
                    break;
                case "give":
                    var give = await _service.SnArticle.Select(c => c.give).ToListAsync();
                    foreach (int item in give)
                    {
                        num += item;
                    }
                    break;
            }

            return num;
        }

        public async Task<List<SnArticle>> GetFyTypeAsync(int type, int pageIndex, int pageSize, string name, bool isDesc)
        {
            result_List = _cacheutil.CacheString("GetFyTypeAsync" + type + pageIndex + pageSize + name + isDesc, result_List); //设置缓存
            if (result_List == null)
            {
                result_List = await GetFyType(type, pageIndex, pageSize, name, isDesc);
                _cacheutil.CacheString("GetFyTypeAsync" + type + pageIndex + pageSize + name + isDesc, result_List);
            }
            return result_List;
        }

        private async Task<List<SnArticle>> GetFyType(int type, int pageIndex, int pageSize, string name, bool isDesc)
        {
            if (isDesc) //降序
            {
                if (type == 00)//表示查所有
                {
                    switch (name)
                    {
                        case "read":
                            return await _service.SnArticle.Where(s => true)
                            .OrderByDescending(c => c.read).Skip((pageIndex - 1) * pageSize)
                           .Take(pageSize).ToListAsync();
                        case "data":
                            return await _service.SnArticle.Where(s => true)
                           .OrderByDescending(c => c.time).Skip((pageIndex - 1) * pageSize)
                           .Take(pageSize).ToListAsync();
                        case "give":
                            return await _service.SnArticle.Where(s => true)
                           .OrderByDescending(c => c.give).Skip((pageIndex - 1) * pageSize)
                           .Take(pageSize).ToListAsync();
                        case "comment":
                            return await _service.SnArticle.Where(s => true)
                           .OrderByDescending(c => c.comment).Skip((pageIndex - 1) * pageSize)
                           .Take(pageSize).ToListAsync();
                        default:
                            return await _service.SnArticle.Where(s => true)
                            .OrderByDescending(c => c.article_id).Skip((pageIndex - 1) * pageSize)
                           .Take(pageSize).ToListAsync();
                    }
                }
                else
                {
                    return await _service.SnArticle.Where(s => s.sort_id == type)
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
                            return await _service.SnArticle.Where(s => true)
                          .OrderBy(c => c.read).Skip((pageIndex - 1) * pageSize)
                          .Take(pageSize).ToListAsync();
                        case "data":
                            return await _service.SnArticle.Where(s => true)
                          .OrderBy(c => c.time).Skip((pageIndex - 1) * pageSize)
                          .Take(pageSize).ToListAsync();
                        case "give":
                            return await _service.SnArticle.Where(s => true)
                          .OrderBy(c => c.give).Skip((pageIndex - 1) * pageSize)
                          .Take(pageSize).ToListAsync();
                        case "comment":
                            return await _service.SnArticle.Where(s => true)
                          .OrderBy(c => c.comment).Skip((pageIndex - 1) * pageSize)
                          .Take(pageSize).ToListAsync();
                        default:
                            return await _service.SnArticle.Where(s => true)
                            .OrderBy(c => c.article_id).Skip((pageIndex - 1) * pageSize)
                            .Take(pageSize).ToListAsync();
                    }
                }
                else
                {
                    return await _service.SnArticle.Where(s => s.sort_id == type)
                     .OrderBy(c => c.article_id).Skip((pageIndex - 1) * pageSize)
                      .Take(pageSize).ToListAsync();
                }

            }
        }

        public async Task<List<SnArticle>> GetFyTitleAsync(int pageIndex, int pageSize, bool isDesc)
        {
            result_List = _cacheutil.CacheString("GetFyTitleAsync" + pageIndex + pageSize + isDesc, result_List); //设置缓存
            if (result_List == null)
            {
                result_List = await GetFyTitle(pageIndex, pageSize, isDesc); //读取数据
                _cacheutil.CacheString("GetFyTitleAsync" + pageIndex + pageSize + isDesc, result_List); //设置缓存
            }
            return result_List;
        }

        /// <summary>
        /// 读取分页数据
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="isDesc"></param>
        /// <returns></returns>
        private async Task<List<SnArticle>> GetFyTitle(int pageIndex, int pageSize, bool isDesc)
        {
            if (isDesc) //降序
            {
                var data = await _service.SnArticle.Where(s => true).Select(s => new
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
                var data = await _service.SnArticle.Where(s => true).Select(s => new
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
            var date = _service.SnArticle.Update(snArticle);

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
            return await _service.SaveChangesAsync() > 0;
        }

        public async Task<List<SnArticle>> GetTagtextAsync(int tag, bool isDesc)
        {
            result_List = _cacheutil.CacheString("GetTagtextAsync" + tag + isDesc, result_List); //设置缓存
            if (result_List == null)
            {
                result_List = await GetTagtext(tag, isDesc); //读取数据
                _cacheutil.CacheString("GetTagtextAsync" + tag + isDesc, result_List); //设置缓存
            }
            return result_List;
        }

        private async Task<List<SnArticle>> GetTagtext(int tag, bool isDesc)
        {
            if (isDesc)
            {
                var data = await _service.SnArticle.Where(s => s.label_id == tag).Select(s => new
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
                var data = await _service.SnArticle.Where(s => s.label_id == tag).Select(s => new
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

    }
}
