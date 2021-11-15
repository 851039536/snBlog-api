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
    public class SnSortService : BaseService, ISnSortService
    {

        private readonly snblogContext _service;
        private readonly CacheUtil _cacheutil;

        private readonly Res<SnSort> res = new();
        private readonly ResDto<SnSortDto> resDto = new();
        private readonly ILogger<SnSort> _logger;
        private readonly IMapper _mapper;
        public SnSortService(IRepositoryFactory repositoryFactory, IConcardContext mydbcontext, snblogContext service, ICacheUtil cacheutil, ILogger<SnSort> logger, IMapper mapper) : base(repositoryFactory, mydbcontext)
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
            var result = await _service.SnSorts.FindAsync(id);
            if (result == null)
            {
                return false;
            }

            _service.SnSorts.Remove(result);
            return await _service.SaveChangesAsync() > 0;
        }

        public async Task<List<SnSort>> AsyGetSort()
        {
            var data = CreateService<SnSort>();
            return await data.GetAll().ToListAsync();
        }

        public async Task<SnSortDto> GetByIdAsync(int id, bool cache)
        {
            _logger.LogInformation("select SnSortDto GetByIdAsync=> id,cache:" + id + cache);
            resDto.entity = _cacheutil.CacheString("GetByIdAsync_SnSort" + id + cache + id, resDto.entity, cache);
            if (res.entity != null)
            {
                return resDto.entity;
            }
            resDto.entity = _mapper.Map<SnSortDto>(await _service.SnSorts.FindAsync(id));
            _cacheutil.CacheString("GetByIdAsync_SnSort" + id + cache, resDto.entity, cache);
            return resDto.entity;
        }

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<bool> AddAsync(SnSort entity)
        {
            await _service.SnSorts.AddAsync(entity);
            return await _service.SaveChangesAsync() > 0;

        }

        public async Task<bool> UpdateAsync(SnSort entity)
        {
            _service.SnSorts.Update(entity);
            return await _service.SaveChangesAsync() > 0;
        }

        public async Task<List<SnSortDto>> GetFyAsync(int pageIndex, int pageSize, bool isDesc, bool cache)
        {
            _logger.LogInformation("select fy int pageIndex, int pageSize, bool isDesc, bool cache" + pageIndex + pageSize + isDesc + cache);
            resDto.entityList = _cacheutil.CacheString("GetFyAllAsync_SnSort" + pageIndex + pageSize + isDesc + cache, resDto.entityList, cache);
            if (res.entityList != null)
            {
                return resDto.entityList;
            }
            await GetFyAll(pageIndex, pageSize, isDesc);
            _cacheutil.CacheString("GetFyAllAsync_SnSort" + pageIndex + pageSize + isDesc + cache, resDto.entityList, cache);
            return resDto.entityList;
        }

        private async Task GetFyAll(int pageIndex, int pageSize, bool isDesc)
        {
            if (isDesc)
            {
                resDto.entityList = _mapper.Map<List<SnSortDto>>(await _service.SnSorts.OrderByDescending(c => c.Id).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync());
            }
            else
            {
                resDto.entityList = _mapper.Map<List<SnSortDto>>(await _service.SnSorts.OrderBy(c => c.Id).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync());
            }
        }


        public async Task<List<SnSortDto>> GetAllAsync(bool cache)
        {
            _logger.LogInformation("select SnSortDto= cache:", cache);
            resDto.entityList = _cacheutil.CacheString("GetAllAsync_SnSort" + cache, resDto.entityList, cache);
            if (resDto.entityList != null)
            {
                return resDto.entityList;
            }
            resDto.entityList = _mapper.Map<List<SnSortDto>>(await _service.SnSorts.AsNoTracking().ToListAsync());
            _cacheutil.CacheString("GetAllAsync_SnSort" + cache, resDto.entityList, cache);
            return resDto.entityList;
        }

        public async Task<int> GetCountAsync(bool cache)
        {
            _logger.LogInformation("SnSortDto查询总数=>" + cache);
            res.entityInt = _cacheutil.CacheNumber("GetCountAsync_SnSort" + cache, res.entityInt, cache);
            if (res.entityInt != 0)
            {
                return res.entityInt;
            }
            res.entityInt = await _service.SnSorts.AsNoTracking().CountAsync();
            _cacheutil.CacheNumber("GetCountAsync_SnSort" + cache, res.entityInt, cache);
            return res.entityInt;
        }
    }
}
