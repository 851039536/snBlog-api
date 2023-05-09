using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Snblog.Cache.CacheUtil;
using Snblog.Enties.Models;
using Snblog.Enties.ModelsDto;
using Snblog.IRepository;
using Snblog.IService;
using Snblog.Repository.Repository;
using Snblog.Util.components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Snblog.Service.Service
{
    public class ArticleTagService : BaseService, IArticleTagService
        {

        private readonly snblogContext _service;
        private readonly CacheUtil _cacheutil;

        private readonly Res<ArticleTag> res = new();
        private readonly Dto<ArticleTagDto> rDto = new();
        private readonly ILogger<ArticleTag> _logger;
        private readonly IMapper _mapper;

        const string NAME = "ArticleTag_";
        const string BYID = "BYID_";
        const string SUM = "SUM_";
        const string CONTAINS = "CONTAINS_";
        const string PAGING = "PAGING_";
        const string ALL = "ALL_";
        const string DEL = "DEL_";
        const string ADD = "ADD_";
        const string UP = "UP_";
        public ArticleTagService(IRepositoryFactory repositoryFactory,IConcardContext mydbcontext,snblogContext service,ICacheUtil cacheutil,ILogger<ArticleTag> logger,IMapper mapper) : base(repositoryFactory,mydbcontext)
            {
            _service = service;
            _cacheutil = (CacheUtil)cacheutil;
            _logger = logger;
            _mapper = mapper;
            }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(int id)
            {
            var result = await _service.ArticleTags.FindAsync(id);
            if (result == null) {
                return false;
                }
            _service.ArticleTags.Remove(result);
            return await _service.SaveChangesAsync() > 0;
            }

        public async Task<List<ArticleType>> AsyGetSort()
            {
            var data = CreateService<ArticleType>();
            return await data.GetAll().ToListAsync();
            }

        /// <summary>
        /// 主键查询
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="cache">缓存</param>
        /// <returns>entity</returns>
        public async Task<ArticleTagDto> GetByIdAsync(int id,bool cache)
            {
            _logger.LogInformation($"{NAME}{BYID}{id}{cache}");
            rDto.entity = _cacheutil.CacheString($"{NAME}{BYID}{id}{cache}{id}",rDto.entity,cache);
            if (res.entity != null) return rDto.entity;
            rDto.entity = _mapper.Map<ArticleTagDto>(await _service.ArticleTags.FindAsync(id));
            _cacheutil.CacheString($"{NAME}{BYID}{id}{cache}",rDto.entity,cache);
            return rDto.entity;
            }

        /// <summary>
        ///  添加
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>bool</returns>
        public async Task<bool> AddAsync(ArticleTag entity)
            {
            await _service.ArticleTags.AddAsync(entity);
            return await _service.SaveChangesAsync() > 0;
            }

        public async Task<bool> UpdateAsync(ArticleTag entity)
            {
            _service.ArticleTags.Update(entity);
            return await _service.SaveChangesAsync() > 0;
            }

        /// <summary>
        /// 分页查询 
        /// </summary>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页记录条数</param>
        /// <param name="isDesc">是否倒序</param>
        /// <param name="cache">缓存</param>
        /// <returns>list-entity</returns>
        public async Task<List<ArticleTagDto>> GetPagingAsync(int pageIndex,int pageSize,bool isDesc,bool cache)
            {
            _logger.LogInformation($"{NAME}{PAGING}{pageIndex}_{pageSize}_{isDesc}_{cache}");
            rDto.entityList = _cacheutil.CacheString($"{NAME}{PAGING}{pageIndex}{pageSize}{isDesc}{cache}",rDto.entityList,cache);
            if (res.entityList != null) return rDto.entityList;
            await QPaging(pageIndex,pageSize,isDesc);
            _cacheutil.CacheString($"{NAME}{PAGING}{pageIndex}{pageSize}{isDesc}{cache}",rDto.entityList,cache);
            return rDto.entityList;
            }

        private async Task QPaging(int pageIndex,int pageSize,bool isDesc)
            {
            if (isDesc) {
                rDto.entityList = _mapper.Map<List<ArticleTagDto>>(await _service.ArticleTags.OrderByDescending(c => c.Id).Skip(( pageIndex - 1 ) * pageSize).Take(pageSize).ToListAsync());
                } else {
                rDto.entityList = _mapper.Map<List<ArticleTagDto>>(await _service.ArticleTags.OrderBy(c => c.Id).Skip(( pageIndex - 1 ) * pageSize).Take(pageSize).ToListAsync());
                }
            }

        public async Task<List<ArticleTagDto>> GetAllAsync(bool cache)
            {
            _logger.LogInformation($"{NAME}{ALL}",cache);
            rDto.entityList = _cacheutil.CacheString($"{NAME}{SUM}{cache}",rDto.entityList,cache);
            if (rDto.entityList != null) return rDto.entityList;
            rDto.entityList = _mapper.Map<List<ArticleTagDto>>(await _service.ArticleTags.AsNoTracking().ToListAsync());
            _cacheutil.CacheString($"{NAME}{SUM}{cache}",rDto.entityList,cache);
            return rDto.entityList;
            }
        /// <summary>
        /// 查询总数
        /// </summary>
        /// <param name="cache">缓存</param>
        /// <returns>int</returns>
        public async Task<int> GetSumAsync(bool cache)
            {
            _logger.LogInformation($"{NAME}{SUM}" + cache);
            res.entityInt = _cacheutil.CacheNumber($"{NAME}{SUM}{cache}",res.entityInt,cache);
            if (res.entityInt != 0) return res.entityInt;
            res.entityInt = await _service.ArticleTags.AsNoTracking().CountAsync();
            _cacheutil.CacheNumber($"{NAME}{SUM}{cache}",res.entityInt,cache);
            return res.entityInt;
            }
        }
    }
