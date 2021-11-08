using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Snblog.Cache.CacheUtil;
using Snblog.Enties.Models;
using Snblog.Enties.ModelsDto;
using Snblog.IService.IService;
using Snblog.Repository.Repository;
using Snblog.Util.components;

namespace Snblog.Service.Service
{
    public class SnNavigationService : ISnNavigationService
    {
        private readonly snblogContext _service;//DB
        private readonly CacheUtil _cacheutil;
        //private int result_Int;
        //private List<SnNavigation> result_List = null;
        //private SnNavigation result_Model = null;
        //private List<SnNavigationDto> result_ListDto = default;

        Tool<SnNavigation> data = new Tool<SnNavigation>();
        Tool<SnNavigationDto> datas = new Tool<SnNavigationDto>();
        private readonly ILogger<SnNavigationService> _logger;
        private readonly IMapper _mapper;
        public SnNavigationService(snblogContext service, ICacheUtil cacheutil, ILogger<SnNavigationService> logger, IMapper mapper)
        {
            _service = service;
            _cacheutil = (CacheUtil)cacheutil;
            _logger = logger;
            _mapper = mapper;
        }


        public async Task<List<SnNavigation>> GetFyAllAsync(string type, int pageIndex, int pageSize, bool isDesc, bool cahe)
        {

            _logger.LogInformation("SnNavigation分页查询=>" + type + pageIndex + pageSize + isDesc + cahe);
            data.resultList = _cacheutil.CacheString("GetFyAllAsync_SnNavigation" + type + pageIndex + pageSize + isDesc + cahe, data.resultList, cahe);
            if (data.resultList == null)
            {
                await GetFyAll(type, pageIndex, pageSize, isDesc);
                _cacheutil.CacheString("GetFyAllAsync_SnNavigation" + type + pageIndex + pageSize + isDesc + cahe, data.resultList, cahe);
            }
            return data.resultList;
        }

        private async Task GetFyAll(string type, int pageIndex, int pageSize, bool isDesc)
        {
            if (type == "all")
            {
                if (isDesc)
                {
                    data.resultList = await _service.SnNavigations.OrderByDescending(c => c.Id).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
                }
                else
                {
                    data.resultList = await _service.SnNavigations.OrderBy(c => c.Id).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
                }
            }
            else
            {
                if (isDesc)
                {
                    data.resultList = await _service.SnNavigations.Where(c => c.Type.Title == type).OrderByDescending(c => c.Id).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
                }
                else
                {
                    data.resultList = await _service.SnNavigations.Where(c => c.Type.Title == type).OrderBy(c => c.Id).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
                }
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(int id)
        {
            _logger.LogInformation("SnNavigation删除数据=>" + id);
            var todoItem = await _service.SnNavigations.FindAsync(id);
            if (todoItem == null) return false;
            _service.SnNavigations.Remove(todoItem);
            return await _service.SaveChangesAsync() > 0;
        }

        public async Task<List<SnNavigation>> GetTypeOrderAsync(string type, bool order, bool cache)
        {
            _logger.LogInformation("SnNavigation条件查询=>" + type + order + cache);
            data.resultList = _cacheutil.CacheString("GetTypeOrderAsync_SnNavigation" + type + order + cache, data.resultList, cache);
            if (data.resultList == null)
            {
                if (order)
                {
                    data.resultList = await _service.SnNavigations.Where(c => c.Type.Title == type).OrderByDescending(c => c.Id).ToListAsync();
                }
                else
                {
                    data.resultList = await _service.SnNavigations.Where(c => c.Type.Title == type).OrderBy(c => c.Id).ToListAsync();
                }
                _cacheutil.CacheString("GetTypeOrderAsync_SnNavigation" + type + order + cache, data.resultList, cache);
            }
            return data.resultList;
        }

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<bool> AddAsync(SnNavigation entity)
        {
            _logger.LogInformation("SnNavigation添加数据=>" + entity);
            await _service.SnNavigations.AddAsync(entity);
            return await _service.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(SnNavigation entity)
        {
            _logger.LogInformation("SnNavigation修改数据=>" + entity);
            _service.SnNavigations.Update(entity);
            return await _service.SaveChangesAsync() > 0;

        }

        public async Task<int> GetCountAsync(int identity, int type, bool cache)
        {

            _logger.LogInformation("SnNavigation查询总数=>" + cache);
            data.resulInt = _cacheutil.CacheNumber("GetCountAsync_SnNavigation" + cache, data.resulInt, cache);
            if (data.resulInt == 0)
            {
                switch (identity)
                {
                    case 0:
                        data.resulInt = await _service.SnNavigations.AsNoTracking().CountAsync();
                        break;
                    case 1:
                        data.resulInt = await _service.SnNavigations.Where(w=>w.TypeId==type).AsNoTracking().CountAsync();
                        break;
                    case 2:
                        data.resulInt = await _service.SnNavigations.Where(w => w.UserId == type).AsNoTracking().CountAsync();
                        break;
                }
                _cacheutil.CacheNumber("GetCountAsync_SnNavigation" + cache, data.resulInt, cache);
            }
            return data.resulInt;
        }


        public async Task<List<SnNavigationDto>> GetAllAsync(bool cache)
        {
            _logger.LogInformation("SnNavigation查询所有=>" + cache);
            datas.resultListDto = _cacheutil.CacheString("GetAllAsync_SnNavigation" + cache, datas.resultListDto, cache);
            if (datas.resultListDto == null)
            {
                datas.resultListDto = _mapper.Map<List<SnNavigationDto>>(await _service.SnNavigations.ToListAsync());
                _cacheutil.CacheString("GetAllAsync_SnNavigation" + cache, datas.resultListDto, cache);
            }
            return datas.resultListDto;
        }



        public async Task<SnNavigationDto> GetByIdAsync(int id, bool cache)
        {
            _logger.LogInformation("SnNavigation主键查询=>" + id + cache);
            datas.resultDto = _cacheutil.CacheString("GetByIdAsync_SnNavigation" + id + cache, datas.resultDto, cache);
            if (datas.resultDto == null)
            {
                datas.resultDto = _mapper.Map<SnNavigationDto>(await _service.SnNavigations.FindAsync(id));
                _cacheutil.CacheString("GetByIdAsync_SnNavigation" + id + cache, data.result, cache);
            }
            return datas.resultDto;
        }

        public async Task<List<SnNavigationDto>> GetContainsAsync(int identity,int type, string name, bool cache)
        {

            _logger.LogInformation(message: $"SnNavigationDto模糊查询=>{type}{name}{cache}");

            datas.resultListDto = _cacheutil.CacheString("GetContainsAsync_SnNavigationDto" + name + cache, datas.resultListDto, cache);
            if (datas.resultListDto == null)
            {
                switch (identity)
                {
                    case 0:
                        datas.resultListDto = _mapper.Map<List<SnNavigationDto>>(
                   data.resultList = await _service.SnNavigations
                 .Where(l => l.Title.Contains(name))
                 .AsNoTracking().ToListAsync());
                        break;
                    case 1:
                        datas.resultListDto = _mapper.Map<List<SnNavigationDto>>(
                   data.resultList = await _service.SnNavigations
                 .Where(l => l.Title.Contains(name) && l.TypeId==type)
                 .AsNoTracking().ToListAsync());
                        break;
                    case 2:
                        datas.resultListDto = _mapper.Map<List<SnNavigationDto>>(
                   data.resultList = await _service.SnNavigations
                 .Where(l => l.Title.Contains(name) && l.UserId==type)
                 .AsNoTracking().ToListAsync());
                        break;
                }
                _cacheutil.CacheString("GetContainsAsync_SnNavigationDto" + name + cache, datas.resultListDto, cache);
            }
            return datas.resultListDto;
        }
      
    }
}
