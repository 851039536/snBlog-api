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
        public async Task<List<SnNavigationDto>> GetFyAsync(int identity, string type, int pageIndex, int pageSize, string ordering, bool isDesc, bool cache)
        {

            _logger.LogInformation("SnNavigation分页查询=>" + identity + pageIndex + pageSize + isDesc + type);
            datas.resultListDto = _cacheutil.CacheString("GetFyAllAsync_SnNavigation" + type + pageIndex + pageSize + isDesc + identity, datas.resultListDto, cache);
            if (datas.resultListDto == null)
            {
                switch (identity) //查询条件
                {
                    case 0:
                        if (isDesc)//降序
                        {
                            switch (ordering) //排序
                            {
                                case "id":
                                    datas.resultListDto = _mapper.Map<List<SnNavigationDto>>(
                            await _service.SnNavigations
                            .OrderByDescending(c => c.Id).Skip((pageIndex - 1) * pageSize)
                            .Take(pageSize).AsNoTracking().ToListAsync());

                                    break;
                                case "data":
                                    datas.resultListDto = _mapper.Map<List<SnNavigationDto>>(await _service.SnNavigations
                            .OrderByDescending(c => c.TimeCreate).Skip((pageIndex - 1) * pageSize)
                            .Take(pageSize).AsNoTracking().ToListAsync());
                                    break;
                            }
                        }
                        else //升序
                        {
                            switch (ordering) //排序
                            {
                                case "id":
                                    datas.resultListDto = _mapper.Map<List<SnNavigationDto>>(
                            await _service.SnNavigations
                            .OrderBy(c => c.Id).Skip((pageIndex - 1) * pageSize)
                            .Take(pageSize).AsNoTracking().ToListAsync());

                                    break;
                                case "data":
                                    datas.resultListDto = _mapper.Map<List<SnNavigationDto>>(await _service.SnNavigations
                            .OrderBy(c => c.TimeCreate).Skip((pageIndex - 1) * pageSize)
                            .Take(pageSize).AsNoTracking().ToListAsync());
                                    break;
                            }
                        }
                        break;

                    case 1:
                        if (isDesc)//降序
                        {
                            switch (ordering) //排序
                            {
                                case "id":
                                    datas.resultListDto = _mapper.Map<List<SnNavigationDto>>(await _service.SnNavigations.Where(w => w.Type.Title == type)
                            .OrderByDescending(c => c.Id).Skip((pageIndex - 1) * pageSize)
                            .Take(pageSize).AsNoTracking().ToListAsync());
                                    break;
                                case "data":
                                    datas.resultListDto = _mapper.Map<List<SnNavigationDto>>(await _service.SnNavigations.Where(w => w.Type.Title == type)
                            .OrderByDescending(c => c.TimeCreate).Skip((pageIndex - 1) * pageSize)
                            .Take(pageSize).AsNoTracking().ToListAsync());
                                    break;
                            }
                        }
                        else //升序
                        {
                            switch (ordering) //排序
                            {
                                case "id":
                                    datas.resultListDto = _mapper.Map<List<SnNavigationDto>>(await _service.SnNavigations.Where(w => w.Type.Title == type)
                            .OrderBy(c => c.Id).Skip((pageIndex - 1) * pageSize)
                            .Take(pageSize).AsNoTracking().ToListAsync());
                                    break;
                                case "data":
                                    datas.resultListDto = _mapper.Map<List<SnNavigationDto>>(await _service.SnNavigations.Where(w => w.Type.Title == type)
                            .OrderBy(c => c.TimeCreate).Skip((pageIndex - 1) * pageSize)
                            .Take(pageSize).AsNoTracking().ToListAsync());
                                    break;
                            }
                        }
                        break;

                    case 2:
                        if (isDesc)//降序
                        {
                            switch (ordering) //排序
                            {
                                case "id":
                                    datas.resultListDto = _mapper.Map<List<SnNavigationDto>>(await _service.SnNavigations.Where(w => w.User.Name == type)
                            .OrderByDescending(c => c.Id).Skip((pageIndex - 1) * pageSize)
                            .Take(pageSize).AsNoTracking().ToListAsync());
                                    break;
                                case "data":
                                    datas.resultListDto = _mapper.Map<List<SnNavigationDto>>(await _service.SnNavigations.Where(w => w.User.Name == type)
                           .OrderByDescending(c => c.TimeCreate).Skip((pageIndex - 1) * pageSize)
                           .Take(pageSize).AsNoTracking().ToListAsync());
                                    break;
                            }
                        }
                        else //升序
                        {
                            switch (ordering) //排序
                            {
                                case "id":
                                    datas.resultListDto = _mapper.Map<List<SnNavigationDto>>(await _service.SnNavigations.Where(w => w.User.Name == type)
                            .OrderBy(c => c.Id).Skip((pageIndex - 1) * pageSize)
                            .Take(pageSize).AsNoTracking().ToListAsync());
                                    break;
                                case "data":
                                    datas.resultListDto = _mapper.Map<List<SnNavigationDto>>(await _service.SnNavigations.Where(w => w.User.Name == type)
                           .OrderBy(c => c.TimeCreate).Skip((pageIndex - 1) * pageSize)
                           .Take(pageSize).AsNoTracking().ToListAsync());
                                    break;
                            }
                        }
                        break;
                }
                data.resultList = _cacheutil.CacheString("GetFyAllAsync_SnNavigation" + type + pageIndex + pageSize + isDesc + identity, data.resultList, cache);
            }
            return datas.resultListDto;
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
        public async Task<List<SnNavigationDto>> GetTypeAsync(int identity, string type, bool cache)
        {
            _logger.LogInformation("SnNavigation条件查询=>" + type + identity + cache);
            datas.resultListDto = _cacheutil.CacheString("GetTypeOrderAsync_SnNavigation" + type + identity + cache, datas.resultListDto, cache);
            if (datas.resultListDto == null)
            {
                switch (identity)
                {
                    case 1:
                        datas.resultListDto = _mapper.Map<List<SnNavigationDto>>(await _service.SnNavigations.Where(w => w.Type.Title == type).AsNoTracking().ToListAsync());
                        break;
                    case 2:
                        datas.resultListDto = _mapper.Map<List<SnNavigationDto>>(await _service.SnNavigations.Where(w => w.User.Name == type).AsNoTracking().ToListAsync());
                        break;
                }
                _cacheutil.CacheString("GetTypeOrderAsync_SnNavigation" + type + identity + cache, datas.resultListDto, cache);
            }
            return datas.resultListDto;
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
        public async Task<int> GetCountAsync(int identity, string type, bool cache)
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
                        data.resulInt = await _service.SnNavigations.Where(w => w.Type.Title == type).AsNoTracking().CountAsync();
                        break;
                    case 2:
                        data.resulInt = await _service.SnNavigations.Where(w => w.User.Name == type).AsNoTracking().CountAsync();
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
        public async Task<List<SnNavigationDto>> GetContainsAsync(int identity, string type, string name, bool cache)
        {

            _logger.LogInformation(message: $"SnNavigationDto模糊查询=>{type}{name}{cache}");

            datas.resultListDto = _cacheutil.CacheString("GetContainsAsync_SnNavigationDto" + name + cache, datas.resultListDto, cache);
            if (datas.resultListDto == null)
            {
                switch (identity)
                {
                    case 0:
                        datas.resultListDto = _mapper.Map<List<SnNavigationDto>>(
                  await _service.SnNavigations
                 .Where(l => l.Title.Contains(name))
                 .AsNoTracking().ToListAsync());
                        break;
                    case 1:
                        datas.resultListDto = _mapper.Map<List<SnNavigationDto>>(
                   await _service.SnNavigations
                 .Where(l => l.Title.Contains(name) && l.Type.Title == type)
                 .AsNoTracking().ToListAsync());
                        break;
                    case 2:
                        datas.resultListDto = _mapper.Map<List<SnNavigationDto>>(
                  await _service.SnNavigations
                 .Where(l => l.Title.Contains(name) && l.User.Name == type)
                 .AsNoTracking().ToListAsync());
                        break;
                }
                _cacheutil.CacheString("GetContainsAsync_SnNavigationDto" + name + cache, datas.resultListDto, cache);
            }
            return datas.resultListDto;
        }

    }
}
