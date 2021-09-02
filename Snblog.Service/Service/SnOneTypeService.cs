using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Snblog.Cache.CacheUtil;
using Snblog.IService.IService;
using Snblog.Models;

namespace Snblog.Service.Service
{
    public class SnOneTypeService : ISnOneTypeService
    {
        private readonly SnblogContext _service;
        private readonly CacheUtil _cacheutil;
        private int result_Int;
        private List<SnOneType> result_List = null;

        private readonly ILogger<SnOneTypeService> _logger;
        public SnOneTypeService(SnblogContext service, ICacheUtil cacheutil, ILogger<SnOneTypeService> logger)
        {
            _service = service;
            _cacheutil = (CacheUtil)cacheutil;
            _logger = logger;
        }

        public async Task<bool> AddAsync(SnOneType entity)
        {
            _logger.LogInformation("添加数据_SnOneType" + entity);
            await _service.SnOneType.AddAsync(entity);
            return await _service.SaveChangesAsync() > 0;
        }

        public async Task<int> CountAsync(bool cache)
        {
            _logger.LogInformation("查询总数_SnOneType" + cache);
            result_Int = _cacheutil.CacheNumber("CountAsync_SnOneType"+cache, result_Int,cache);
            if (result_Int != 0)
            {
                return result_Int;
            }
            result_Int = await _service.SnOneType.CountAsync();
            _cacheutil.CacheNumber("CountAsync_SnOneType"+cache, result_Int,cache);
            return result_Int;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            _logger.LogInformation("删除数据_SnOneType" + id);
            var result = await _service.SnOneType.FindAsync(id);
            _service.SnOneType.Remove(result);
            return await _service.SaveChangesAsync() > 0;
        }

        public async Task<List<SnOneType>> GetAllAsync(bool cache)
        {
            _logger.LogInformation("查询所有_SnOneType"+ cache);
            result_List = _cacheutil.CacheString("GetAllAsync_SnOneType"+cache, result_List,cache);
            if (result_List != null)
            {
                return result_List;
            }
            result_List = await _service.SnOneType.ToListAsync();
            _cacheutil.CacheString("GetAllAsync_SnOneType"+cache, result_List,cache);
            return result_List;
        }

        public async Task<SnOneType> GetByIdAsync(int id, bool cache)
        {
            _logger.LogInformation("主键查询_SnOneType" + cache);
            SnOneType result = default;
            result = _cacheutil.CacheString("GetByIdAsync_SnOneType" + id+cache, result,cache);
            if (result != null)
            {
                return result;
            }
            result = await _service.SnOneType.FindAsync(id);
            _cacheutil.CacheString("GetByIdAsync_SnOneType" + id+cache, result,cache);
            return result;
        }

        public async Task<SnOneType> GetTypeAsync(int type, bool cache)
        {
            _logger.LogInformation("类别查询_SnOneType"+type+cache);
            SnOneType result = default;
            result = _cacheutil.CacheString("GetTypeAsync_SnOneType" + type+cache, result,cache);
            if (result != null)
            {
                return result;
            }
            result = await _service.SnOneType.FirstAsync(s => s.SoTypeId == type);
            _cacheutil.CacheString("GetTypeAsync_SnOneType" + type+cache, result,cache);
            return result;
        }

        public async Task<bool> UpdateAsync(SnOneType entity)
        {
            _logger.LogInformation("更新数据_SnOneType" + entity);
            _service.SnOneType.Update(entity);
            return await _service.SaveChangesAsync() > 0;
        }
    }
}
