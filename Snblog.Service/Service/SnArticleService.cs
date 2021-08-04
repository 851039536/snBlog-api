using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Serilog.Core;
using Snblog.Cache.CacheUtil;
using Snblog.IService.IService;
using Snblog.Models;

namespace Snblog.Service.Service
{
    public class SnArticleService : ISnArticleService
    {
        private readonly snblogContext _service;
        private readonly CacheUtil _cacheutil;
        private readonly ILogger<SnArticleService> _logger;
        private int result_Int;
        private List<SnArticle> result_List = default;
        private SnArticleDto resultDto = default;
        private List<SnArticleDto> result_ListDto = default;
        // 创建一个字段来存储mapper对象
        private readonly IMapper _mapper;
        public SnArticleService(ICacheUtil cacheUtil, snblogContext coreDbContext, ILogger<SnArticleService> logger, IMapper mapper)
        {
            _service = coreDbContext;
            _cacheutil = (CacheUtil)cacheUtil;
            _logger = logger ?? throw new ArgumentNullException(nameof(Logger));
            _mapper = mapper;
        }


        public async Task<bool> DeleteAsync(int id)
        {
            _logger.LogInformation("删除数据_SnArticle" + id);
            //先查询出来，因为只能删除被追踪的数据
            var todoItem = await _service.SnArticle.FindAsync(id);
            if (todoItem == null) return false;
            //1、单独删除方法
            _service.SnArticle.Remove(todoItem);//删除单个Leagues
            _service.Remove(todoItem);//直接在context上Remove()方法传入model，它会判断类型
            return await _service.SaveChangesAsync() > 0;
        }

        public async Task<SnArticleDto> GetByIdAsync(int id, bool cache)
        {
            _logger.LogInformation("主键查询_SnArticleDto" + id + cache);
            SnArticle result = null;
            resultDto = _cacheutil.CacheString("GetByIdAsync_SnArticleDto" + id + cache, resultDto, cache);
            if (resultDto == null)
            {
                result = await _service.SnArticle.FindAsync(id);
                resultDto = _mapper.Map<SnArticleDto>(result);
                _cacheutil.CacheString("GetByIdAsync_SnArticleDto" + id + cache, resultDto, cache);
            }
            return resultDto;
        }


        public async Task<List<SnArticle>> GetTypeIdAsync(int sortId, bool cache)
        {
            _logger.LogInformation("分类条件查询_SnArticle" + sortId + cache);
            result_List = _cacheutil.CacheString("GetTypeIdAsync_SnArticle" + sortId + cache, result_List, cache);
            if (result_List == null)
            {
                result_List = await _service.SnArticle.Where(s => s.LabelId == sortId).ToListAsync();
                _cacheutil.CacheString("GetTypeIdAsync_SnArticle" + sortId + cache, result_List, cache);
            }
            return result_List;
        }


        public async Task<List<SnArticle>> GetfyTestAsync(int label, int pageIndex, int pageSize, bool isDesc, bool cache)
        {
            _logger.LogInformation("按标签分页查询_SnArticle :" + label + pageIndex + pageSize + isDesc + cache);
            result_List = _cacheutil.CacheString("GetfyTestAsync_SnArticle" + label + pageIndex + pageSize + isDesc + cache, result_List, cache);
            if (result_List == null)
            {
                result_List = await GetfyTest(label, pageIndex, pageSize, isDesc);
                _cacheutil.CacheString("GetfyTestAsync_SnArticle" + label + pageIndex + pageSize + isDesc + cache, result_List, cache);
            }
            return result_List;
        }
        /// <summary>
        /// 按分类分页查询 
        /// </summary>
        /// <param name="sort"></param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页记录条数</param>
        /// <param name="isDesc">是否倒序</param>
        public async Task<List<SnArticle>> GetfySortTestAsync(int sort, int pageIndex, int pageSize, bool isDesc, bool cache)
        {
            _logger.LogInformation("条件分页查询_SnArticle" + sort + pageIndex + pageSize + isDesc + cache);
            result_List = _cacheutil.CacheString("GetfySortTestAsync_SnArticle" + sort + pageIndex + pageSize + isDesc + cache, result_List, cache);
            if (result_List == null)
            {
                result_List = await GetfySortTest(sort, pageIndex, pageSize, isDesc);
                _cacheutil.CacheString("GetfySortTestAsync_SnArticle" + sort + pageIndex + pageSize + isDesc + cache, result_List, cache);
            }
            return result_List;
        }
        private async Task<List<SnArticle>> GetfySortTest(int sort, int pageIndex, int pageSize, bool isDesc)
        {
            if (sort == 00)
            {
                if (isDesc)
                {
                    result_List = await _service.SnArticle.OrderByDescending(c => c.ArticleId).Skip((pageIndex - 1) * pageSize)
                           .Take(pageSize).ToListAsync();
                }
                else
                {
                    result_List = await _service.SnArticle.OrderBy(c => c.ArticleId).Skip((pageIndex - 1) * pageSize)
                             .Take(pageSize).ToListAsync();
                }
            }
            else
            {
                if (isDesc)
                {
                    result_List = await _service.SnArticle.Where(s => s.SortId == sort).OrderByDescending(c => c.Read).Skip((pageIndex - 1) * pageSize)
                            .Take(pageSize).ToListAsync();
                }
                else
                {
                    result_List = await _service.SnArticle.Where(s => s.SortId == sort).OrderBy(c => c.Read).Skip((pageIndex - 1) * pageSize)
                           .Take(pageSize).ToListAsync();
                }
            }
            return result_List;
        }

        private async Task<List<SnArticle>> GetfyTest(int label, int pageIndex, int pageSize, bool isDesc)
        {
            if (label == 00)
            {
                if (isDesc)
                {
                    result_List = await _service.SnArticle.OrderByDescending(c => c.ArticleId).Skip((pageIndex - 1) * pageSize)
                           .Take(pageSize).ToListAsync();
                }
                else
                {
                    result_List = await _service.SnArticle.OrderBy(c => c.ArticleId).Skip((pageIndex - 1) * pageSize)
                             .Take(pageSize).ToListAsync();
                }
            }
            else
            {
                if (isDesc)
                {
                    result_List = await _service.SnArticle.Where(s => s.LabelId == label).OrderByDescending(c => c.ArticleId).Skip((pageIndex - 1) * pageSize)
                            .Take(pageSize).ToListAsync();
                }
                else
                {
                    result_List = await _service.SnArticle.Where(s => s.LabelId == label).OrderBy(c => c.ArticleId).Skip((pageIndex - 1) * pageSize)
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
            _logger.LogInformation("添加数据_SnArticle" + entity);
            await _service.SnArticle.AddAsync(entity);
            return await _service.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(SnArticle entity)
        {
            _logger.LogInformation("更新数据_SnArticle" + entity);
            _service.SnArticle.Update(entity);
            return await _service.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// 查询标签总数
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public int GetTypeCountAsync(int type, bool cache)
        {
            _logger.LogInformation("查询标签总数_SnArticle" + type + cache);
            result_Int = _cacheutil.CacheNumber("GetTypeCountAsync_SnArticle" + type + cache, result_Int, cache);
            if (result_Int == 0)
            {
                result_Int = _service.SnArticle.Count(c => c.LabelId == type);
                _cacheutil.CacheNumber("GetTypeCountAsync_SnArticle" + type + cache, result_Int, cache);
            }
            return result_Int;
        }
        /// <summary>
        /// 查询分类总数
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public async Task<int> GetConutSortAsync(int type, bool cache)
        {
            _logger.LogInformation("查询分类总数_SnArticle:" + type + cache);
            result_Int = _cacheutil.CacheNumber("GetConutSortAsync_SnArticle" + type + cache, result_Int, cache);
            if (result_Int == 0)
            {
                result_Int = await _service.SnArticle.CountAsync(c => c.SortId == type);
                _cacheutil.CacheNumber("GetConutSortAsync_SnArticle" + type + cache, result_Int, cache);
            }
            return result_Int;
        }

        public async Task<int> CountAsync(bool cache)
        {
            _logger.LogInformation("查询总数_SnArticle" + cache);
            result_Int = _cacheutil.CacheNumber("Count_SnArticle" + cache, result_Int, cache);
            if (result_Int == 0)
            {
                result_Int = await _service.SnArticle.CountAsync();
                _cacheutil.CacheNumber("Count_SnArticle" + cache, result_Int, cache);
            }
            return result_Int;
        }

        public async Task<List<SnArticleDto>> GetAllAsync(bool cache)
        {
            _logger.LogInformation("查询所有_SnArticleDto" + cache);
            result_ListDto = _cacheutil.CacheString("GetAllAsync_SnArticleDto" + cache, result_ListDto, cache);
            if (result_ListDto == null)
            {
                result_ListDto = _mapper.Map<List<SnArticleDto>>(await _service.SnArticle.ToListAsync());
                _cacheutil.CacheString("GetAllAsync_SnArticleDto" + cache, result_ListDto, cache);
            }
            return result_ListDto;
        }

        public async Task<int> GetSumAsync(string type, bool cache)
        {
            _logger.LogInformation("统计[字段/阅读/点赞]数量__SnArticle" + type + cache);
            result_Int = _cacheutil.CacheNumber("GetSumAsync_SnArticle" + type + cache, result_Int, cache);
            if (result_Int == 0)
            {
                result_Int = await GetSum(type);
                _cacheutil.CacheNumber("GetSumAsync_SnArticle" + type + cache, result_Int, cache);
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
            _logger.LogInformation("读取总字数：" + type);
            int num = 0;
            switch (type) //按类型查询
            {
                case "read":
                    var read = await _service.SnArticle.Select(c => c.Read).ToListAsync();
                    foreach (var i in read)
                    {
                        num += i;
                    }
                    break;
                case "text":
                    var text = await _service.SnArticle.Select(c => c.Text).ToListAsync();
                    for (int i = 0; i < text.Count; i++)
                    {
                        num += text[i].Length;
                    }
                    break;
                case "give":
                    var give = await _service.SnArticle.Select(c => c.Give).ToListAsync();
                    foreach (var i in give)
                    {
                        num += i;
                    }
                    break;
            }

            return num;
        }

        public async Task<List<SnArticle>> GetFyAsync(int type, int pageIndex, int pageSize, string name, bool isDesc, bool cache)
        {
            _logger.LogInformation("分页查询(条件排序)_SnArticle" + type + pageIndex + pageSize + name + isDesc + cache);
            result_List = _cacheutil.CacheString("GetFyAsync_SnArticle" + type + pageIndex + pageSize + name + isDesc + cache, result_List, cache); //设置缓存
            if (result_List == null)
            {
                result_List = await GetFyType(type, pageIndex, pageSize, name, isDesc);
                _cacheutil.CacheString("GetFyAsync_SnArticle" + type + pageIndex + pageSize + name + isDesc + cache, result_List, cache);
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
                            .OrderByDescending(c => c.Read).Skip((pageIndex - 1) * pageSize)
                           .Take(pageSize).ToListAsync();
                        case "data":
                            return await _service.SnArticle.Where(s => true)
                           .OrderByDescending(c => c.TimeCreate).Skip((pageIndex - 1) * pageSize)
                           .Take(pageSize).ToListAsync();
                        case "give":
                            return await _service.SnArticle.Where(s => true)
                           .OrderByDescending(c => c.Give).Skip((pageIndex - 1) * pageSize)
                           .Take(pageSize).ToListAsync();
                        case "comment":
                            return await _service.SnArticle.Where(s => true)
                           .OrderByDescending(c => c.Comment).Skip((pageIndex - 1) * pageSize)
                           .Take(pageSize).ToListAsync();
                        default:
                            return await _service.SnArticle.Where(s => true)
                            .OrderByDescending(c => c.ArticleId).Skip((pageIndex - 1) * pageSize)
                           .Take(pageSize).ToListAsync();
                    }
                }
                else
                {
                    return await _service.SnArticle.Where(s => s.SortId == type)
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
                            return await _service.SnArticle.Where(s => true)
                          .OrderBy(c => c.Read).Skip((pageIndex - 1) * pageSize)
                          .Take(pageSize).ToListAsync();
                        case "data":
                            return await _service.SnArticle.Where(s => true)
                          .OrderBy(c => c.TimeCreate).Skip((pageIndex - 1) * pageSize)
                          .Take(pageSize).ToListAsync();
                        case "give":
                            return await _service.SnArticle.Where(s => true)
                          .OrderBy(c => c.Give).Skip((pageIndex - 1) * pageSize)
                          .Take(pageSize).ToListAsync();
                        case "comment":
                            return await _service.SnArticle.Where(s => true)
                          .OrderBy(c => c.Comment).Skip((pageIndex - 1) * pageSize)
                          .Take(pageSize).ToListAsync();
                        default:
                            return await _service.SnArticle.Where(s => true)
                            .OrderBy(c => c.ArticleId).Skip((pageIndex - 1) * pageSize)
                            .Take(pageSize).ToListAsync();
                    }
                }
                else
                {
                    return await _service.SnArticle.Where(s => s.SortId == type)
                     .OrderBy(c => c.ArticleId).Skip((pageIndex - 1) * pageSize)
                      .Take(pageSize).ToListAsync();
                }

            }
        }

        public async Task<List<SnArticle>> GetFyTitleAsync(int pageIndex, int pageSize, bool isDesc, bool cache)
        {
            _logger.LogInformation("查询文章(无文章内容)_SnArticle");
            result_List = _cacheutil.CacheString("GetFyTitleAsync_SnArticle" + pageIndex + pageSize + isDesc + cache, result_List, cache); //设置缓存
            if (result_List == null)
            {
                result_List = await GetFyTitle(pageIndex, pageSize, isDesc); //读取数据
                _cacheutil.CacheString("GetFyTitleAsync_SnArticle" + pageIndex + pageSize + isDesc + cache, result_List, cache); //设置缓存
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
            _logger.LogInformation("读取分页数据");
            if (isDesc) //降序
            {
                var data = await _service.SnArticle.Where(s => true).Select(s => new
                {
                    s.ArticleId,
                    s.Title,
                    s.Comment,
                    s.Give,
                    s.Read,
                    s.TimeCreate,
                    s.TitleText,
                    s.UserId
                }).OrderByDescending(c => c.ArticleId).Skip((pageIndex - 1) * pageSize)
                             .Take(pageSize).ToListAsync();

                //解决方案二：foreach遍历
                var list = new List<SnArticle>();
                foreach (var t in data)
                {
                    var s = new SnArticle();
                    s.ArticleId = t.ArticleId;
                    s.Title = t.Title;
                    s.Comment = t.Comment;
                    s.Give = t.Give;
                    s.Read = t.Read;
                    s.TimeCreate = t.TimeCreate;
                    s.TitleText = t.TitleText;
                    s.UserId = t.UserId;
                    list.Add(s);
                }
                return list;
            }
            else
            {
                var data = await _service.SnArticle.Where(s => true).Select(s => new
                {
                    s.ArticleId,
                    s.Title,
                    s.Comment,
                    s.Give,
                    s.Read,
                    s.TimeCreate,
                    s.TitleText,
                    s.UserId
                }).OrderBy(c => c.ArticleId).Skip((pageIndex - 1) * pageSize)
                         .Take(pageSize).ToListAsync();
                var list = new List<SnArticle>();
                foreach (var t in data)
                {
                    var s = new SnArticle();
                    s.ArticleId = t.ArticleId;
                    s.Title = t.Title;
                    s.Comment = t.Comment;
                    s.Give = t.Give;
                    s.Read = t.Read;
                    s.TimeCreate = t.TimeCreate;
                    s.TitleText = t.TitleText;
                    s.UserId = t.UserId;
                    list.Add(s);
                }
                return list;
            }
        }

        public async Task<bool> UpdatePortionAsync(SnArticle snArticle, string type)
        {
            _logger.LogInformation("更新部分参数");
            var resulet = await _service.SnArticle.FindAsync(snArticle.ArticleId);
            if (resulet == null) return false;
            switch (type)
            {    //指定字段进行更新操作
                case "Read":
                    //修改属性，被追踪的league状态属性就会变为Modify
                    resulet.Read = snArticle.Read;
                    break;
                case "Give":
                    resulet.Give = snArticle.Give;
                    break;
                case "Comment":
                    resulet.Comment = snArticle.Comment;
                    break;
            }
            //执行数据库操作
            return await _service.SaveChangesAsync() > 0;
        }

        public async Task<List<SnArticle>> GetTagAsync(int tag, bool isDesc, bool cache)
        {
            _logger.LogInformation("按标签条件查询_SnArticle" + tag + isDesc + cache);
            result_List = _cacheutil.CacheString("GetTagAsync_SnArticle" + tag + isDesc + cache, result_List, cache); //设置缓存
            if (result_List == null)
            {
                result_List = await GetTagtext(tag, isDesc); //读取数据
                _cacheutil.CacheString("GetTagAsync_SnArticle" + tag + isDesc + cache, result_List, cache); //设置缓存
            }
            return result_List;
        }

        private async Task<List<SnArticle>> GetTagtext(int tag, bool isDesc)
        {
            if (isDesc)
            {
                var data = await _service.SnArticle.Where(s => s.LabelId == tag).Select(s => new
                {
                    s.ArticleId,
                    s.Title,
                    s.TitleText,
                    s.TimeCreate,
                    s.Give,
                    s.Read
                }).OrderByDescending(s => s.ArticleId).ToListAsync();
                var list = new List<SnArticle>();
                foreach (var t in data)
                {
                    var s = new SnArticle();
                    s.ArticleId = t.ArticleId;
                    s.Title = t.Title;
                    s.TitleText = t.TitleText;
                    s.TimeCreate = t.TimeCreate;
                    s.Give = t.Give;
                    s.Read = t.Read;
                    list.Add(s);
                }
                return list;
            }
            else
            {
                var data = await _service.SnArticle.Where(s => s.LabelId == tag).Select(s => new
                {
                    s.ArticleId,
                    s.Title,
                    s.TitleText,
                    s.TimeCreate,
                    s.Give,
                    s.Read
                }).OrderBy(s => s.ArticleId).ToListAsync();
                var list = new List<SnArticle>();
                foreach (var t in data)
                {
                    var s = new SnArticle();
                    s.ArticleId = t.ArticleId;
                    s.Title = t.Title;
                    s.TitleText = t.TitleText;
                    s.TimeCreate = t.TimeCreate;
                    s.Give = t.Give;
                    s.Read = t.Read;
                    list.Add(s);
                }
                return list;
            }
        }

        public async Task<List<SnArticleDto>> GetContainsAsync(string name, bool cache)
        {
            _logger.LogInformation("模糊查询_SnArticleDto" + name + cache);
            result_ListDto = _cacheutil.CacheString("GetContainsAsync_SnArticleDto" + name + cache, result_ListDto, cache); //设置缓存
            if (result_ListDto == null)
            {
                result_ListDto = _mapper.Map<List<SnArticleDto>>(
                    result_List = await _service.SnArticle
                   .Where(l => l.Title.Contains(name))//查询条件
                   .ToListAsync());

                _cacheutil.CacheString("GetContainsAsync_SnArticleDto" + name + cache, result_ListDto, cache); //设置缓存
            }
            return result_ListDto;
        }
    }
}
