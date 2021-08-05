using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Snblog.Cache.CacheUtil;
using Snblog.IRepository;
using Snblog.IService.IService;
using Snblog.Models;
using Snblog.Repository.Repository;

namespace Snblog.Service.Service
{
    public class SnNavigationService : ISnNavigationService
    {
        private readonly SnblogContext _service;//DB
        private readonly CacheUtil _cacheutil;
        private int result_Int;
        private List<SnNavigation> result_List = null;
        private SnNavigation result_Model = null;
        private readonly ILogger<SnNavigationService> _logger;
        public SnNavigationService(SnblogContext service, ICacheUtil cacheutil, ILogger<SnNavigationService> logger)
        {
            _service = service;
            _cacheutil = (CacheUtil)cacheutil;
            _logger = logger;
        }


        public async Task<List<SnNavigation>> GetFyAllAsync(string type, int pageIndex, int pageSize, bool isDesc, bool cahe)
        {

            _logger.LogInformation("分页查询_SnNavigation" + type + pageIndex + pageSize + isDesc + cahe);
            result_List = _cacheutil.CacheString("GetFyAllAsync_SnNavigation" + type + pageIndex + pageSize + isDesc + cahe, result_List, cahe);
            if (result_List == null)
            {
                await GetFyAll(type, pageIndex, pageSize, isDesc);
                _cacheutil.CacheString("GetFyAllAsync_SnNavigation" + type + pageIndex + pageSize + isDesc + cahe, result_List, cahe);
            }
            return result_List;
        }

        private async Task GetFyAll(string type, int pageIndex, int pageSize, bool isDesc)
        {
            if (type == "all")
            {
                if (isDesc)
                {
                    result_List = await _service.SnNavigation.OrderByDescending(c => c.NavId).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
                }
                else
                {
                    result_List = await _service.SnNavigation.OrderBy(c => c.NavId).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
                }
            }
            else
            {
                if (isDesc)
                {
                    result_List = await _service.SnNavigation.Where(c => c.NavType == type).OrderByDescending(c => c.NavId).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
                }
                else
                {
                    result_List = await _service.SnNavigation.Where(c => c.NavType == type).OrderBy(c => c.NavId).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
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
            _logger.LogInformation("删除数据_SnNavigation" + id);
            var todoItem = await _service.SnNavigation.FindAsync(id);
            if (todoItem == null) return false;
            _service.SnNavigation.Remove(todoItem);
            return await _service.SaveChangesAsync() > 0;

        }




        public async Task<List<SnNavigation>> GetTypeOrderAsync(string type, bool order, bool cache)
        {
            _logger.LogInformation("条件查询_SnNavigation" + type + order + cache);
            result_List = _cacheutil.CacheString("GetTypeOrderAsync_SnNavigation" + type + order + cache, result_List, cache);
            if (result_List == null)
            {
                if (order)
                {
                    result_List = await _service.SnNavigation.Where(c => c.NavType == type).OrderByDescending(c => c.NavId).ToListAsync();
                }
                else
                {
                    result_List = await _service.SnNavigation.Where(c => c.NavType == type).OrderBy(c => c.NavId).ToListAsync();
                }
                _cacheutil.CacheString("GetTypeOrderAsync_SnNavigation" + type + order + cache, result_List, cache);
            }
            return result_List;
        }

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<bool> AddAsync(SnNavigation entity)
        {
            _logger.LogInformation("添加数据_SnNavigation" + entity);
            await _service.SnNavigation.AddAsync(entity);
            return await _service.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(SnNavigation entity)
        {
            _logger.LogInformation("修改数据_SnNavigation" + entity);
            _service.SnNavigation.Update(entity);
            return await _service.SaveChangesAsync() > 0;

        }

        public async Task<int> GetCountAsync(bool cache)
        {
            _logger.LogInformation("查询总数_SnNavigation" + cache);
            result_Int = _cacheutil.CacheNumber("GetCountAsync_SnNavigation" + cache, result_Int, cache);
            if (result_Int == 0)
            {
                result_Int = await _service.SnNavigation.CountAsync();
                _cacheutil.CacheNumber("GetCountAsync_SnNavigation" + cache, result_Int, cache);
            }
            return result_Int;
        }

        public async Task<int> CountTypeAsync(string type, bool cache)
        {
            _logger.LogInformation("条件查询总数_SnNavigation" + type + cache);
            result_Int = _cacheutil.CacheNumber("CountTypeAsync_SnNavigation" + type + cache, result_Int, cache);
            if (result_Int == 0)
            {
                result_Int = await _service.SnNavigation.CountAsync(c => c.NavType == type);
                _cacheutil.CacheNumber("CountTypeAsync_SnNavigation" + type + cache, result_Int, cache);
            }
            return result_Int;
        }


        public async Task<List<SnNavigation>> GetAllAsync(bool cache)
        {
            _logger.LogInformation("查询所有_SnNavigation" + cache);
            result_List = _cacheutil.CacheString("GetAllAsync_SnNavigation" + cache, result_List, cache);
            if (result_List == null)
            {
                result_List = await _service.SnNavigation.ToListAsync();
                _cacheutil.CacheString("GetAllAsync_SnNavigation" + cache, result_List, cache);
            }
            return result_List;
        }

        public async Task<List<SnNavigation>> GetDistinct(string type)
        {
            result_List = _cacheutil.CacheString1("GetDistinct" + type, result_List);
            if (result_List == null)
            {
                result_List = await _service.SnNavigation.Distinct().Where(c => c.NavType == type).ToListAsync();
                _cacheutil.CacheString1("GetDistinct" + type, result_List);
            }
            return result_List;

        }

        public async Task<SnNavigation> GetByIdAsync(int id, bool cache)
        {
            _logger.LogInformation("主键查询_SnNavigation" + id + cache);
            result_Model = _cacheutil.CacheString("GetByIdAsync_SnNavigation" + id + cache, result_Model, cache);
            if (result_Model == null)
            {
                result_Model = await _service.SnNavigation.FindAsync(id);
                _cacheutil.CacheString("GetByIdAsync_SnNavigation" + id + cache, result_Model, cache);
            }

            return result_Model;
        }
    }
}
