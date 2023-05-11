using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Snblog.Cache.CacheUtil;
using Snblog.Enties.Models;
using Snblog.IService.IService;
using Snblog.Repository.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Snblog.Service.Service
{
    public class DiaryTypeService : IDiaryTypeService
    {
        private readonly snblogContext _service;
        private readonly CacheUtil _cacheutil;
        private int result_Int;
        private List<DiaryType> result_List = null;

        private readonly ILogger<DiaryTypeService> _logger;
        public DiaryTypeService(snblogContext service, ICacheUtil cacheutil, ILogger<DiaryTypeService> logger)
        {
            _service = service;
            _cacheutil = (CacheUtil)cacheutil;
            _logger = logger;
        }

        public async Task<bool> AddAsync(DiaryType entity)
        {
            Log.Information("添加数据_SnOneType" + entity);
            await _service.DiaryTypes.AddAsync(entity);
            return await _service.SaveChangesAsync() > 0;
        }

        public async Task<int> CountAsync(bool cache)
        {
            Log.Information("查询总数_SnOneType" + cache);
            result_Int = _cacheutil.CacheNumber("CountAsync_SnOneType" + cache, result_Int, cache);
            if (result_Int != 0)
            {
                return result_Int;
            }
            result_Int = await _service.DiaryTypes.CountAsync();
            _cacheutil.CacheNumber("CountAsync_SnOneType" + cache, result_Int, cache);
            return result_Int;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            Log.Information("删除数据_SnOneType" + id);
            var result = await _service.DiaryTypes.FindAsync(id);
            _service.DiaryTypes.Remove(result);
            return await _service.SaveChangesAsync() > 0;
        }

        public async Task<List<DiaryType>> GetAllAsync(bool cache)
        {
            Log.Information("查询所有_SnOneType" + cache);
            result_List = _cacheutil.CacheString("GetAllAsync_SnOneType" + cache, result_List, cache);
            if (result_List != null)
            {
                return result_List;
            }
            result_List = await _service.DiaryTypes.ToListAsync();
            _cacheutil.CacheString("GetAllAsync_SnOneType" + cache, result_List, cache);
            return result_List;
        }

        public async Task<DiaryType> GetByIdAsync(int id, bool cache)
        {
            Log.Information("主键查询_SnOneType" + cache);
            DiaryType result = default;
            result = _cacheutil.CacheString("GetByIdAsync_SnOneType" + id + cache, result, cache);
            if (result != null)
            {
                return result;
            }
            result = await _service.DiaryTypes.FindAsync(id);
            _cacheutil.CacheString("GetByIdAsync_SnOneType" + id + cache, result, cache);
            return result;
        }

        public async Task<DiaryType> GetTypeAsync(int type, bool cache)
        {
            Log.Information("类别查询_SnOneType" + type + cache);
            DiaryType result = default;
            result = _cacheutil.CacheString("GetTypeAsync_SnOneType" + type + cache, result, cache);
            if (result != null)
            {
                return result;
            }
            result = await _service.DiaryTypes.FirstAsync(s => s.Id == type);
            _cacheutil.CacheString("GetTypeAsync_SnOneType" + type + cache, result, cache);
            return result;
        }

        public async Task<bool> UpdateAsync(DiaryType entity)
        {
            Log.Information("更新数据_SnOneType" + entity);
            _service.DiaryTypes.Update(entity);
            return await _service.SaveChangesAsync() > 0;
        }
    }
}
