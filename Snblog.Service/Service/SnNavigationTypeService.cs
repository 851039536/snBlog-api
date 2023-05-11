using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Snblog.Cache.CacheUtil;
using Snblog.Enties.Models;
using Snblog.IService.IService;
using Snblog.Repository.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Snblog.Service.Service
{
    public class SnNavigationTypeService : ISnNavigationTypeService
    {

        private readonly ILogger<SnNavigationTypeService> _logger;
        private readonly snblogContext _service;//DB
        private readonly CacheUtil _cacheutil;
        private int result_Int;
        private List<SnNavigationType> result_List = null;
        public SnNavigationTypeService(snblogContext service, ICacheUtil cacheutil, ILogger<SnNavigationTypeService> logger)
        {
            _service = service;
            _cacheutil = (CacheUtil)cacheutil;
            _logger = logger;
        }

        public async Task<bool> AddAsync(SnNavigationType entity)
        {

            Log.Information("添加数据_SnNavigationType" + entity);
            await _service.SnNavigationTypes.AddAsync(entity);
            return await _service.SaveChangesAsync() > 0;
        }

        public async Task<int> CountAsync(bool cache)
        {
            Log.Information("查询总数_SnNavigationType" + cache);
            result_Int = _cacheutil.CacheNumber("CountAsync_SnNavigationType" + cache, result_Int, cache);
            if (result_Int == 0)
            {
                result_Int = await _service.SnNavigationTypes.CountAsync();
                _cacheutil.CacheNumber("CountAsync_SnNavigationType" + cache, result_Int, cache);
            }
            return result_Int;
        }

        public async Task<bool> DeleteAsync(int id)
        {

            Log.Information("删除数据_SnNavigationType" + id);
            var todoItem = await _service.SnNavigationTypes.FindAsync(id);
            _service.SnNavigationTypes.Remove(todoItem);
            return await _service.SaveChangesAsync() > 0;
        }

        public async Task<List<SnNavigationType>> GetAllAsync(bool cache)
        {
            Log.Information("查询所有_SnNavigationType" + cache);
            result_List = _cacheutil.CacheString("查询所有_SnNavigationType" + cache, result_List, cache);
            if (result_List == null)
            {
                result_List = await _service.SnNavigationTypes.ToListAsync();
                _cacheutil.CacheString("SnNavigationTypeGetAllAsync" + cache, result_List, cache);
            }
            return result_List;
        }

        public async Task<SnNavigationType> GetByIdAsync(int id, bool cache)
        {
            Log.Information("主键查询_SnNavigationType" + id + cache);
            SnNavigationType result = default;
            result = _cacheutil.CacheString("GetByIdAsync" + id + cache, result, cache);
            if (result == null)
            {
                result = await _service.SnNavigationTypes.FindAsync(id);
                _cacheutil.CacheString("GetByIdAsync" + id + cache, result, cache);
            }
            return result;
        }

        public async Task<List<SnNavigationType>> GetFyTypeAllAsync(string type, int pageIndex, int pageSize, bool isDesc, bool cache)
        {
            Log.Information("条件分页查询_SnNavigationType" + type + pageIndex + pageSize + isDesc, cache);
            result_List = _cacheutil.CacheString("GetFyTypeAllAsync_SnNavigationType" + type + pageIndex + pageSize + isDesc + cache, result_List, cache);
            if (result_List == null)
            {
                result_List = await GetFyTypeAll(type, pageIndex, pageSize, isDesc);
                _cacheutil.CacheString("GetFyTypeAllAsync_SnNavigationType" + type + pageIndex + pageSize + isDesc + cache, result_List, cache);
            }
            return result_List;
        }

        private async Task<List<SnNavigationType>> GetFyTypeAll(string type, int pageIndex, int pageSize, bool isDesc)
        {
            if (isDesc)
            {
                if (type.Equals("all"))
                {
                    return await _service.SnNavigationTypes.Where(s => true).OrderByDescending(c => c.Id).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
                }
                else
                {
                    return await _service.SnNavigationTypes.Where(s => s.Title == type).OrderByDescending(c => c.Id).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
                }
            }
            else
            {
                if (type.Equals("all"))
                {
                    return await _service.SnNavigationTypes.Where(s => s.Title == type).OrderBy(c => c.Id).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
                }
                else
                {
                    return await _service.SnNavigationTypes.Where(s => true).OrderBy(c => c.Id).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
                }
            }
        }

        public async Task<bool> UpdateAsync(SnNavigationType entity)
        {
            Log.Information("更新数据_SnNavigationType" + entity);
            _service.SnNavigationTypes.Update(entity);
            return await _service.SaveChangesAsync() > 0;
        }
    }
}
