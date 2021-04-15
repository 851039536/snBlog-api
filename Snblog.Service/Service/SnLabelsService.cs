using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Snblog.Cache.CacheUtil;
using Snblog.IService;
using Snblog.Models;

namespace Snblog.Service.Service
{
    public class SnLabelsService : ISnLabelsService
    {
        private readonly snblogContext _service;//DB
        private readonly CacheUtil _cacheUtil;
        private int result_Int;
        private List<SnLabels> result_List = null;
        public SnLabelsService(ICacheUtil cacheUtil, snblogContext coreDbContext) 
        {
            _service = coreDbContext;
            _cacheUtil = (CacheUtil)cacheUtil;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(int id)
        {
             var todoItem = await _service.SnLabels.FindAsync(id);
            if (todoItem == null) return false;
            _service.SnLabels.Remove(todoItem);
            return await _service.SaveChangesAsync() > 0;
        }

        public async Task<SnLabels> GetByIdAsync(int id)
        {
            SnLabels labels = null;
            labels = _cacheUtil.CacheString("GetByIdAsync" + id, labels);
            if (labels == null)
            {
                labels = await _service.SnLabels.FindAsync(id);
                _cacheUtil.CacheString("GetByIdAsync" + id, labels);
            }
            return labels;
        }

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<bool> AddAsync(SnLabels entity)
        {
            await _service.SnLabels.AddAsync(entity);
            return await _service.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(SnLabels entity)
        {
            _service.SnLabels.Update(entity);
             return await _service.SaveChangesAsync()>0;
            
        }

        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        public async Task<List<SnLabels>> GetAllAsync()
        {
            result_List = _cacheUtil.CacheString("GetAllAsync", result_List);
            if (result_List == null)
            {
                result_List = await _service.SnLabels.ToListAsync();
                _cacheUtil.CacheString("GetAllAsync", result_List);
            }
            return result_List;
        }

        public async Task<int> GetCountAsync()
        {
            result_Int = _cacheUtil.CacheNumber("GetCountAsync", result_Int);
            if (result_Int == 0)
            {
                result_Int = await _service.SnLabels.CountAsync();
                _cacheUtil.CacheNumber("GetCountAsync", result_Int);
            }
            return result_Int;
        }

        public async Task<List<SnLabels>> GetfyAllAsync(int pageIndex, int pageSize, bool isDesc)
        {

            result_List = _cacheUtil.CacheString("GetfyAllAsync" + pageIndex + pageSize + isDesc, result_List);
            if (result_List == null)
            {
                await GetfyAll(pageIndex, pageSize, isDesc);
                _cacheUtil.CacheString("GetfyAllAsync" + pageIndex + pageSize + isDesc, result_List);
            }
            return result_List;

        }
        private async Task GetfyAll(int pageIndex, int pageSize, bool isDesc)
        {
            if (isDesc)
            {
                result_List = await _service.SnLabels.OrderByDescending(c => c.LabelId).Skip((pageIndex - 1) * pageSize)
                       .Take(pageSize).ToListAsync();
            }
            else
            {
                result_List = await _service.SnLabels.OrderBy(c => c.LabelId).Skip((pageIndex - 1) * pageSize)
                         .Take(pageSize).ToListAsync();
            }
        }
    }
}
