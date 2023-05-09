using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using MySqlX.XDevAPI.Common;
using Snblog.Cache.CacheUtil;
using Snblog.Enties.Models;
using Snblog.Enties.ModelsDto;
using Snblog.IService.IService;
using Snblog.Repository.Repository;
using Snblog.Util.components;
using Snblog.Util.verification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Snblog.Service.Service
{
    public class ArticleService : IArticleService
    {
        private readonly snblogContext _service;
        private readonly CacheUtil _cache;
        private readonly ILogger<ArticleService> _logger;
        private readonly Res<Article> res = new();
        private readonly Dto<ArticleDto> resDto = new();
        private readonly IMapper _mapper;

        string cacheKey;
        const string NAME = "article_";
        const string BYID = "BYID_";
        const string SUM = "SUM_";
        const string CONTAINS = "CONTAINS_";
        const string PAGING = "PAGING_";
        const string ALL = "ALL_";
        const string DEL = "DEL_";
        const string ADD = "ADD_";
        const string UP = "UP_";
        public ArticleService(ICacheUtil cache,snblogContext coreDbContext,ILogger<ArticleService> logger,IMapper mapper)
        {
            _service = coreDbContext;
            _cache = (CacheUtil)cache;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            _logger.LogInformation($"{NAME}{DEL}{id}");
            Article reslult = await _service.Articles.FindAsync(id);
            if (reslult == null) return false;
            _service.Articles.Remove(reslult);//删除单个
            _service.Remove(reslult);//直接在context上Remove()方法传入model，它会判断类型
            return await _service.SaveChangesAsync() > 0;
        }
        public async Task<ArticleDto> GetByIdAsync(int id,bool cache)
        {
            cacheKey = $"{NAME}{BYID}{id}_{cache}";
            _logger.LogInformation(cacheKey);

            if (cache) {
                resDto.entity = _cache.GetValue(cacheKey,resDto.entity);
                if (resDto.entity != null) return resDto.entity;
            }
          
            resDto.entity = _mapper.Map<ArticleDto>(
                await _service.Articles
                .Include(i => i.User)
                .Include(i => i.Type)
                .Include(i => i.Tag)
                .AsNoTracking()
                .SingleOrDefaultAsync(b => b.Id == id));
            _cache.SetValue(cacheKey,resDto.entity);
            return resDto.entity;
        }
        public async Task<List<ArticleDto>> GetTypeAsync(int identity,string type,bool cache)
        {
            cacheKey = $"{NAME}{identity}{type}{cache}";
            _logger.LogInformation(cacheKey);

            if (cache) {
                resDto.entityList = _cache.GetValue(cacheKey,resDto.entityList);
                if (resDto.entityList != null) {
                    return resDto.entityList;
                }
            }

            if (identity == 1) { //分类
                resDto.entityList = _mapper.Map<List<ArticleDto>>(
                    await _service.Articles.Where(s => s.Type.Name == type)
                    .AsNoTracking()
                    .ToListAsync());
            } else { //2 标签
                resDto.entityList = _mapper.Map<List<ArticleDto>>(
                    await _service.Articles.Where(s => s.Tag.Name == type)
                    .AsNoTracking()
                    .ToListAsync());
            }
            _cache.SetValue(cacheKey,resDto.entityList);
            return resDto.entityList;
        }

        public async Task<bool> AddAsync(Article entity)
        {
            cacheKey = $"{NAME}{ADD}{entity}";
            _logger.LogInformation(cacheKey);

            entity.TimeCreate = entity.TimeModified = DateTime.Now;
            //AddAsync 方法中的异步添加改为同步添加，因为 SaveChangesAsync 方法已经是异步的，不需要再使用异步添加
            _service.Articles.Add(entity);
            return await _service.SaveChangesAsync() > 0;
        }
        public async Task<bool> UpdateAsync(Article entity)
        {
            _logger.LogInformation($"{NAME}{UP}{entity}");
            entity.TimeModified = DateTime.Now; //更新时间

            var res = await _service.Articles.Where(w => w.Id == entity.Id).Select(
                s => new {
                    s.TimeCreate,
                }
                ).AsNoTracking().ToListAsync();

            entity.TimeCreate = res[0].TimeCreate;  //赋值表示更新时间不变
            _service.Articles.Update(entity);
            return await _service.SaveChangesAsync() > 0;

            //待更新此优化
            //var res = await _service.Articles.Where(w => w.Id == entity.Id)
            //                     .Select(s => s.TimeCreate)
            //                     .FirstOrDefaultAsync();
            //entity.TimeCreate = res;  //赋值表示更新时间不变
            //_service.Articles.Update(entity);
            //return await _service.SaveChangesAsync() > 0;
            // 优化说明：
            // 1. 将查询 TimeCreate 的代码简化为只查询一个字段。
            // 2. 使用 FirstOrDefaultAsync 方法代替 ToListAsync 方法，因为只需要查询一个字段。
        }
        public async Task<int> GetSumAsync(int identity,string type,bool cache)
        {
            cacheKey = $"{NAME}{SUM}{identity}{type}{cache}";
            _logger.LogInformation(cacheKey);

            
            if (cache) {
                int sum = _cache.GetValue(cacheKey,0);
                if (sum != 0) {  //通过entityInt 值是否为 0 判断结果是否被缓存
                    return sum;
                }
            }

            return identity switch { // case 
                0 => await GetArticleCountAsync(),// 读取文章数量，无需筛选条件
                1 => await GetArticleCountAsync(c => c.Type.Name == type),
                2 => await GetArticleCountAsync(c => c.Tag.Name == type),
                3 => await GetArticleCountAsync(c => c.User.Name == type),
                _ => -1, //default
            };
        }


        /// <summary>
        /// 获取文章的数量
        /// </summary>
        /// <param name="predicate">筛选文章的条件</param>
        /// <returns>返回文章的数量</returns>
        private async Task<int> GetArticleCountAsync(Expression<Func<Article,bool>> predicate = null)
        {
            IQueryable<Article> query = _service.Articles.AsNoTracking();

            //如果有筛选条件
            if (predicate != null) query = query.Where(predicate);

            int count = await query.CountAsync();
            _cache.SetValue(cacheKey,count); //设置缓存
            return count;
        }

        public async Task<List<ArticleDto>> GetAllAsync(bool cache)
        {
            cacheKey = $"{NAME}{ALL}{cache}";
            _logger.LogInformation(cacheKey);

            if (cache) {
                resDto.entityList = _cache.GetValue(cacheKey,resDto.entityList);
                if (resDto.entityList != null) return resDto.entityList;
            }

            resDto.entityList = _mapper.Map<List<ArticleDto>>(
                await _service.Articles.Include(i => i.User)
                .Include(i => i.Tag)
                .Include(i => i.Type)
                .AsNoTracking().ToListAsync());
            _cache.SetValue(cacheKey,resDto.entityList);

            return resDto.entityList;
        }

        /// <summary>
        /// 内容统计
        /// </summary>
        /// <param name="identity">所有:0|分类:1|标签:2|用户:3</param>
        /// <param name="type">内容:1|阅读:2|点赞:3</param>
        /// <param name="name">查询参数</param>
        /// <param name="cache">缓存</param>
        /// <returns>int</returns>
        public async Task<int> GetStrSumAsync(int identity,int type,string name,bool cache)
        {
            cacheKey = $"{NAME}统计{identity}_{type}_{name}_{cache}";
            _logger.LogInformation(cacheKey);

            if (cache) {
                res.entityInt = _cache.GetValue(cacheKey,res.entityInt);
                if (res.entityInt != 0) {
                    return res.entityInt;
                }
            }

            switch (identity) {
                case 0:
                res.entityInt = await GetStatistic(type);

                break;
                case 1:
                res.entityInt = await GetStatistic(type,c => c.Type.Name == name);

                break;
                case 2:
                res.entityInt = await GetStatistic(type,c => c.Tag.Name == name);

                break;
                case 3:
                res.entityInt = await GetStatistic(type,c => c.User.Name == name);
                break;
            }
            _cache.SetValue(cacheKey,res.entityInt);
            return res.entityInt;
        }



        /// <summary>
        /// 读取内容数量
        /// </summary>
        /// <param name="type">内容:1|阅读:2|点赞:3</param>
        /// <returns></returns>
        private async Task<int> GetStatistic(int type,Expression<Func<Article,bool>> predicate = null)
        {
            IQueryable<Article> query = _service.Articles;

            if (predicate != null) {
                query = query.Where(predicate);
            }

            return type switch {
                1 => await query.SumAsync(c => c.Text.Length),
                2 => await query.SumAsync(c => c.Read),
                3 => await query.SumAsync(c => c.Give),
                _ => 0,
            };
        }


        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="identity">所有:0|分类:1|标签:2|用户:3|标签+用户:4</param>
        /// <param name="type">查询参数(多条件以','分割)</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页记录条数</param>
        /// <param name="isDesc">排序</param>
        /// <param name="cache">缓存</param>
        /// <param name="ordering">排序规则 data:时间|read:阅读|give:点赞|id:主键</param>
        public async Task<List<ArticleDto>> GetPagingAsync(int identity,string type,int pageIndex,int pageSize,string ordering,bool isDesc,bool cache)
        {
            cacheKey = $"{NAME}{PAGING}{identity}_{type}_{pageIndex}_{pageSize}_{ordering}_{isDesc}_{cache}";
            _logger.LogInformation(cacheKey);

            if (cache) {
                resDto.entityList = _cache.GetValue(cacheKey,resDto.entityList);
                if (resDto.entityList != null) {
                    return resDto.entityList;
                }
            }
         

            switch (identity) {
                case 0:
                return await GetArticlePaging(pageIndex,pageSize,ordering,isDesc);

                case 1:
                return await GetArticlePaging(pageIndex,pageSize,ordering,isDesc,w => w.Type.Name == type);

                case 2:
                return await GetArticlePaging(pageIndex,pageSize,ordering,isDesc,w => w.Tag.Name == type);
                case 3:

                return await GetArticlePaging(pageIndex,pageSize,ordering,isDesc,w => w.User.Name == type);
                case 4:
                resDto.name = type.Split(',');
                return await GetArticlePaging(pageIndex,pageSize,ordering,isDesc,w => w.Tag.Name == resDto.name[0]
                    && w.User.Name == resDto.name[1]);

                default:
                return await GetArticlePaging(pageIndex,pageSize,ordering,isDesc);

            }
        }

        private async Task<List<ArticleDto>> GetArticlePaging(int pageIndex,int pageSize,string ordering,bool isDesc,Expression<Func<Article,bool>> predicate = null)
        {
            IQueryable<Article> articles = _service.Articles.AsQueryable();

            if (predicate != null) {
                articles = articles.Where(predicate);
            }
            switch (ordering) {
                case "id":
                articles = isDesc ? articles.OrderByDescending(c => c.Id) : articles.OrderBy(c => c.Id);
                break;
                case "data":
                articles = isDesc ? articles.OrderByDescending(c => c.TimeCreate) : articles.OrderBy(c => c.TimeCreate);
                break;
                case "read":
                articles = isDesc ? articles.OrderByDescending(c => c.Read) : articles.OrderBy(c => c.Read);
                break;
                case "give":
                articles = isDesc ? articles.OrderByDescending(c => c.Give) : articles.OrderBy(c => c.Give);
                break;
            }
            var articleDtos = await articles.Skip(( pageIndex - 1 ) * pageSize).Take(pageSize)
            .Select(e => new ArticleDto {
                Id = e.Id,
                Name = e.Name,
                Sketch = e.Sketch,
                Give = e.Give,
                Read = e.Read,
                Img = e.Img,
                TimeCreate = e.TimeCreate,
                TimeModified = e.TimeModified,
                User = e.User,
                Type = e.Type,
                Tag = e.Tag
            }).AsNoTracking().ToListAsync();
            resDto.entityList = _mapper.Map<List<ArticleDto>>(articleDtos);
            _cache.SetValue(cacheKey,resDto.entityList);
            return resDto.entityList;
        }


        public async Task<bool> UpdatePortionAsync(Article entity,string type)
        {
            _logger.LogInformation("Article更新部分参数");
            var result = await _service.Articles.FindAsync(entity.Id);
            if (result == null) return false;

            switch (type) {    //指定字段进行更新操作
                case "Read":
                //修改属性，被追踪的league状态属性就会变为Modify
                result.Read = entity.Read;
                break;
                case "Give":
                result.Give = entity.Give;
                break;
                case "Comment":
                result.CommentId = entity.CommentId;
                break;
                default:
                return false;
            }
            //执行数据库操作
            await _service.SaveChangesAsync();
             //await _service.SaveChangesAsync() > 0;
            return true;
        }

        public async Task<List<ArticleDto>> GetContainsAsync(int identity,string type,string name,bool cache)
        {
            var upNames = name.ToUpper();
            cacheKey = $"{NAME}{CONTAINS}{identity}{type}{name}{cache}";
            _logger.LogInformation(cacheKey);

            if (cache) {
                resDto.entityList = _cache.GetValue(cacheKey,resDto.entityList);
                if (resDto.entityList != null) {
                    return resDto.entityList;
                }
            }
           
            return identity switch {
                0 => await GetArticleContainsAsync(l => l.Name.ToUpper().Contains(upNames)),
                1 => await GetArticleContainsAsync(l => l.Name.ToUpper().Contains(upNames) && l.Type.Name == type),
                2 => await GetArticleContainsAsync(l => l.Name.ToUpper().Contains(upNames) && l.Tag.Name == type),
                _ => await GetArticleContainsAsync(l => l.Name.ToUpper().Contains(upNames)),
            };
        }
    
        /// <summary>
        /// 模糊查询
        /// </summary>
        /// <param name="predicate">筛选文章的条件</param>
        private async Task<List<ArticleDto>> GetArticleContainsAsync(Expression<Func<Article,bool>> predicate = null)
        {
            IQueryable<Article> query = _service.Articles.AsNoTracking();
            if (predicate != null) {
                resDto.entityList = await   query.Where(predicate).Select(e => new ArticleDto {
                    Id = e.Id,
                    Name = e.Name,
                    Sketch = e.Sketch,
                    Give = e.Give,
                    Read = e.Read,
                    Img = e.Img,
                    TimeCreate = e.TimeCreate,
                    TimeModified = e.TimeModified,
                    User = e.User,
                    Type = e.Type,
                    Tag = e.Tag
                }).ToListAsync();
                _cache.SetValue(cacheKey,resDto.entityList); //设置缓存
            }
            return resDto.entityList;
        }
         
    }
}
