using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Snblog.Cache.CacheUtil;
using Snblog.Enties.Models;
using Snblog.IService.IService;
using Snblog.Repository.Repository;

namespace Snblog.Service.Service
{
    public class SnLabelsService : ISnLabelsService
    {
        private readonly snblogContext _service;//DB
        private readonly CacheUtil _cacheUtil;
        private int result_Int;
        private List<SnLabel> result_List = null;
        private readonly ILogger<SnLabelsService> _logger;

        public SnLabelsService(ICacheUtil cacheUtil, snblogContext coreDbContext, ILogger<SnLabelsService> logger)
        {
            _service = coreDbContext;
            _cacheUtil = (CacheUtil)cacheUtil;
            _logger = logger;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(int id)
        {
            _logger.LogInformation("删除数据_SnLabels" + id);
            var todoItem = await _service.SnLabels.FindAsync(id);
            if (todoItem == null) return false;
            _service.SnLabels.Remove(todoItem);
            return await _service.SaveChangesAsync() > 0;
        }

        public async Task<SnLabel> GetByIdAsync(int id,bool cache)
        {

            _logger.LogInformation("主键查询_SnLabels" + id+cache);
            SnLabel labels = null;
            labels = _cacheUtil.CacheString("GetByIdAsync_SnLabels" + id+cache, labels,cache);
            if (labels == null)
            {
                labels = await _service.SnLabels.FindAsync(id);
                _cacheUtil.CacheString("GetByIdAsync_SnLabels" + id+cache, labels,cache);
            }
            return labels;
        }

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<bool> AddAsync(SnLabel entity)
        {
            _logger.LogInformation("添加数据_SnLabels" + entity);
            await _service.SnLabels.AddAsync(entity);
            return await _service.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(SnLabel entity)
        {
            _logger.LogInformation("更新数据_SnLabels" + entity);
            _service.SnLabels.Update(entity);
             return await _service.SaveChangesAsync()>0;
            
        }

        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        public async Task<List<SnLabel>> GetAllAsync(bool cache)
        {

            _logger.LogInformation("查询所有_SnLabels" + cache);
            result_List = _cacheUtil.CacheString("GetAllAsync_SnLabels"+cache, result_List,cache);
            if (result_List == null)
            {
                result_List = await _service.SnLabels.ToListAsync();
                _cacheUtil.CacheString("GetAllAsync_SnLabels"+cache, result_List,cache);
            }
            return result_List;
        }

        public async Task<int> GetCountAsync(bool cache)
        {
            _logger.LogInformation("查询总条数_SnLabels" + cache);
            result_Int = _cacheUtil.CacheNumber("GetCountAsync__SnLabels"+cache, result_Int,cache);
            if (result_Int == 0)
            {
                result_Int = await _service.SnLabels.CountAsync();
                _cacheUtil.CacheNumber("GetCountAsync__SnLabels"+cache, result_Int, cache);
            }
            return result_Int;
        }

        public async Task<List<SnLabel>> GetfyAllAsync(int pageIndex, int pageSize, bool isDesc, bool cache)
        {
            _logger.LogInformation("条件分页查询_SnLabels" + pageIndex+pageSize+isDesc+cache);

            result_List = _cacheUtil.CacheString("GetfyAllAsync_SnLabels" + pageIndex + pageSize + isDesc+cache, result_List,cache);
            if (result_List == null)
            {
                await GetfyAll(pageIndex, pageSize, isDesc);
                _cacheUtil.CacheString("GetfyAllAsync_SnLabels" + pageIndex + pageSize + isDesc+cache, result_List,cache);
            }
            return result_List;

        }
        private async Task GetfyAll(int pageIndex, int pageSize, bool isDesc)
        {
            if (isDesc)
            {
                result_List = await _service.SnLabels.OrderByDescending(c => c.Id).Skip((pageIndex - 1) * pageSize)
                       .Take(pageSize).ToListAsync();
            }
            else
            {
                result_List = await _service.SnLabels.OrderBy(c => c.Id).Skip((pageIndex - 1) * pageSize)
                         .Take(pageSize).ToListAsync();
            }
        }
    }
}
