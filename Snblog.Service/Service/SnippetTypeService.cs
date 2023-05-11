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
    public class SnippetTypeService : BaseService, ISnippetTypeService
        {

        private readonly snblogContext _service;
        private readonly CacheUtil _cacheutil;

        private readonly EntityData<SnippetType> res = new();
        private readonly EntityDataDto<SnippetTypeDto> rDto = new();
        private readonly ILogger<SnippetType> _logger;
        private readonly IMapper _mapper;

        const string NAME = "SnippetType_";
        const string BYID = "BYID_";
        const string SUM = "SUM_";
        const string CONTAINS = "CONTAINS_";
        const string PAGING = "PAGING_";
        const string ALL = "ALL_";
        const string DEL = "DEL_";
        const string ADD = "ADD_";
        const string UP = "UP_";
        public SnippetTypeService(IRepositoryFactory repositoryFactory, IConcardContext mydbcontext, snblogContext service, ICacheUtil cacheutil, ILogger<SnippetType> logger, IMapper mapper) : base(repositoryFactory, mydbcontext)
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

        public async Task<List<SnippetTypeDto>> AsyGetSort()
        {
            var data = CreateService<SnippetTypeDto>();
            return await data.GetAll().ToListAsync();
        }

        /// <summary>
        /// 主键查询
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="cache">缓存</param>
        /// <returns>entity</returns>
        public async Task<SnippetTypeDto> GetByIdAsync(int id, bool cache)
        {
            Log.Information($"{NAME}{BYID}{id}_{cache}");
            rDto.Entity = _cacheutil.CacheString($"{NAME}{BYID}{id}{cache}{id}", rDto.Entity, cache);
            if (res.Entity != null) return rDto.Entity;
            rDto.Entity = _mapper.Map<SnippetTypeDto>(await _service.SnippetTypes.FindAsync(id));
            _cacheutil.CacheString($"{NAME}{BYID}{id}{cache}", rDto.Entity, cache);
            return rDto.Entity;
        }

        /// <summary>
        ///  添加 
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>bool</returns>
        public async Task<bool> AddAsync(SnippetType entity)
        {
            await _service.SnippetTypes.AddAsync(entity);
            return await _service.SaveChangesAsync() > 0;

        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>bool</returns>
        public async Task<bool> UpdateAsync(SnippetType entity)
        {
            _service.SnippetTypes.Update(entity);
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
        public async Task<List<SnippetTypeDto>> GetPagingAsync(int pageIndex, int pageSize, bool isDesc, bool cache)
        {
            Log.Information($"{NAME}{PAGING}{pageIndex}_{pageSize}_{isDesc}_{cache}");
            rDto.EntityList = _cacheutil.CacheString($"{NAME}{PAGING}{pageIndex}{pageSize}{isDesc}{cache}", rDto.EntityList, cache);
            if (res.EntityList != null) return rDto.EntityList;
            //await QPaging(pageIndex, pageSize, isDesc);
            if (isDesc) {
               rDto.EntityList = _mapper.Map<List<SnippetTypeDto>>(await _service.SnippetTypes.OrderByDescending(c => c.Id).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync());
                } else {
               rDto.EntityList = _mapper.Map<List<SnippetTypeDto>>(await _service.SnippetTypes.OrderBy(c => c.Id).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync());
                }
            _cacheutil.CacheString($"{NAME}{PAGING}{pageIndex}{pageSize}{isDesc}{cache}", rDto.EntityList, cache);
            return rDto.EntityList;
        }

        /// <summary>
        /// 查询所有
        /// </summary>
        /// <param name="cache">缓存</param>
        /// <returns>list-entity</returns>
        public async Task<List<SnippetTypeDto>> GetAllAsync(bool cache)
        {
            Log.Information($"{NAME}{ALL}", cache);
            rDto.EntityList = _cacheutil.CacheString($"{NAME}{ALL}{cache}", rDto.EntityList, cache);
            if (rDto.EntityList != null) return rDto.EntityList;
            rDto.EntityList = _mapper.Map<List<SnippetTypeDto>>(await _service.SnippetTypes.AsNoTracking().ToListAsync());
            _cacheutil.CacheString($"{NAME}{ALL}{cache}", rDto.EntityList, cache);
            return rDto.EntityList;
        }
        /// <summary>
        /// 查询总数
        /// </summary>
        /// <param name="cache">缓存</param>
        /// <returns>int</returns>
        public async Task<int> GetSumAsync(bool cache)
        {
            Log.Information($"{NAME}{SUM}{cache}");
            res.EntityCount = _cacheutil.CacheNumber($"{NAME}{SUM}{cache}", res.EntityCount, cache);
            if (res.EntityCount != 0) return res.EntityCount;
            res.EntityCount = await _service.SnippetTypes.AsNoTracking().CountAsync();
            _cacheutil.CacheNumber($"{NAME}{SUM}{cache}", res.EntityCount, cache);
            return res.EntityCount;
        }
    }
}
