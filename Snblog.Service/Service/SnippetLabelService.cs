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
    public class SnippetLabelService : BaseService, ISnippetLabelService
        {

        private readonly snblogContext _service;
        private readonly CacheUtil _cacheutil;

        private readonly Res<SnippetLabel> res = new();
        private readonly Dto<SnippetLabelDto> rDto = new();
        private readonly ILogger<SnippetLabel> _logger;
        private readonly IMapper _mapper;

        const string NAME = "SnippetLabel_";
        const string BYID = "BYID_";
        const string SUM = "SUM_";
        const string CONTAINS = "CONTAINS_";
        const string PAGING = "PAGING_";
        const string ALL = "ALL_";
        const string DEL = "DEL_";
        const string ADD = "ADD_";
        const string UP = "UP_";
        public SnippetLabelService(IRepositoryFactory repositoryFactory, IConcardContext mydbcontext, snblogContext service, ICacheUtil cacheutil, ILogger<SnippetLabel> logger, IMapper mapper) : base(repositoryFactory, mydbcontext)
        {
            _service = service;
            _cacheutil = (CacheUtil)cacheutil;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>bool</returns>
        public async Task<bool> DeleteAsync(int id)
        {
            var result = await _service.SnippetTypes.FindAsync(id);
            if (result == null) return false;
            _service.SnippetTypes.Remove(result);
            return await _service.SaveChangesAsync() > 0;
        }

        public async Task<List<SnippetLabelDto>> AsyGetSort()
        {
            var data = CreateService<SnippetLabelDto>();
            return await data.GetAll().ToListAsync();
        }

        /// <summary>
        /// 主键查询
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="cache">缓存</param>
        /// <returns>entity</returns>
        public async Task<SnippetLabelDto> GetByIdAsync(int id, bool cache)
        {
            _logger.LogInformation($"{NAME}{BYID}{id}_{cache}");
            rDto.entity = _cacheutil.CacheString($"{NAME}{BYID}{id}{cache}{id}", rDto.entity, cache);
            if (res.entity != null) return rDto.entity;
            rDto.entity = _mapper.Map<SnippetLabelDto>(await _service.SnippetLabels.FindAsync(id));
            _cacheutil.CacheString($"{NAME}{BYID}{id}{cache}", rDto.entity, cache);
            return rDto.entity;
        }

        /// <summary>
        ///  添加 
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>bool</returns>
        public async Task<bool> AddAsync(SnippetLabel entity)
        {
            await _service.SnippetLabels.AddAsync(entity);
            return await _service.SaveChangesAsync() > 0;

        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>bool</returns>
        public async Task<bool> UpdateAsync(SnippetLabel entity)
        {
            _service.SnippetLabels.Update(entity);
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
        public async Task<List<SnippetLabelDto>> GetPagingAsync(int pageIndex, int pageSize, bool isDesc, bool cache)
        {
            _logger.LogInformation($"{NAME}{PAGING}{pageIndex}_{pageSize}_{isDesc}_{cache}");
            rDto.eList = _cacheutil.CacheString($"{NAME}{PAGING}{pageIndex}{pageSize}{isDesc}{cache}", rDto.eList, cache);
            if (res.entityList != null) return rDto.eList;
            //await QPaging(pageIndex, pageSize, isDesc);
            if (isDesc) {
               rDto.eList = _mapper.Map<List<SnippetLabelDto>>(await _service.SnippetLabels.OrderByDescending(c => c.Id).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync());
                } else {
               rDto.eList = _mapper.Map<List<SnippetLabelDto>>(await _service.SnippetLabels.OrderBy(c => c.Id).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync());
                }
            _cacheutil.CacheString($"{NAME}{PAGING}{pageIndex}{pageSize}{isDesc}{cache}", rDto.eList, cache);
            return rDto.eList;
        }

        /// <summary>
        /// 查询所有
        /// </summary>
        /// <param name="cache">缓存</param>
        /// <returns>list-entity</returns>
        public async Task<List<SnippetLabelDto>> GetAllAsync(bool cache)
        {
            _logger.LogInformation($"{NAME}{ALL}", cache);
            rDto.eList = _cacheutil.CacheString($"{NAME}{ALL}{cache}", rDto.eList, cache);
            if (rDto.eList != null) return rDto.eList;
            rDto.eList = _mapper.Map<List<SnippetLabelDto>>(await _service.SnippetLabels.AsNoTracking().ToListAsync());
            _cacheutil.CacheString($"{NAME}{ALL}{cache}", rDto.eList, cache);
            return rDto.eList;
        }
        /// <summary>
        /// 查询总数
        /// </summary>
        /// <param name="cache">缓存</param>
        /// <returns>int</returns>
        public async Task<int> GetSumAsync(bool cache)
        {
            _logger.LogInformation($"{NAME}{SUM}{cache}");
            res.entityInt = _cacheutil.CacheNumber($"{NAME}{SUM}{cache}", res.entityInt, cache);
            if (res.entityInt != 0) return res.entityInt;
            res.entityInt = await _service.SnippetLabels.AsNoTracking().CountAsync();
            _cacheutil.CacheNumber($"{NAME}{SUM}{cache}", res.entityInt, cache);
            return res.entityInt;
        }
    }
}
