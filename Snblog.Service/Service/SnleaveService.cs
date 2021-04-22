using Microsoft.EntityFrameworkCore;
using Snblog.IService.IService;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Snblog.Enties.Models;
using Snblog.Cache.CacheUtil;
using Snblog.Repository.Repository;

namespace Snblog.Service.Service
{
    public class SnleaveService : ISnleaveService
    {
        private readonly snblogContext _service;//DB
        private readonly CacheUtil _cacheutil;
        private int result_Int;
        private List<SnLeave> result_List = null;
        public SnleaveService(snblogContext service, ICacheUtil cacheutil)
        {
            _service = service;
            _cacheutil = (CacheUtil)cacheutil;
        }

        public async Task<bool> AddAsync(SnLeave entity)
        {
            await _service.SnLeave.AddAsync(entity);
            return await _service.SaveChangesAsync() > 0;
        }

        public async Task<int> CountAsync()
        {
            result_Int = _cacheutil.CacheNumber("CountAsyncSnLeave", result_Int);
            if (result_Int == 0)
            {
                result_Int = await _service.SnLeave.CountAsync();
                _cacheutil.CacheNumber("CountAsyncSnLeave", result_Int);
            }
            return result_Int;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var todoItem = await _service.SnLeave.FindAsync(id);
            _service.SnLeave.Remove(todoItem);
            return await _service.SaveChangesAsync() > 0;
        }

        public async Task<List<SnLeave>> GetAllAsync()
        {
            result_List = _cacheutil.CacheString("GetAllAsyncSnLeave", result_List);
            if (result_List == null)
            {
                result_List = await _service.SnLeave.ToListAsync();
                _cacheutil.CacheString("GetAllAsyncSnLeave", result_List);
            }
            return result_List;
        }

        public async Task<SnLeave> GetByIdAsync(int id)
        {
            SnLeave result = default;
            result = _cacheutil.CacheString("GetByIdAsyncSnLeave", result);
            if (result == null)
            {
                result = await _service.SnLeave.FindAsync(id);
                _cacheutil.CacheString("GetByIdAsyncSnLeave", result);
            }
            return result;
        }

        public async Task<List<SnLeave>> GetFyAllAsync(int pageIndex, int pageSize, bool isDesc)
        {
            result_List = _cacheutil.CacheString("GetFyAllAsyncSnLeave", result_List);
            if (result_List == null)
            {
                result_List = await GetFyListAsync(pageIndex, pageSize, isDesc);
                _cacheutil.CacheString("GetFyAllAsyncSnLeave", result_List);
            }
            return result_List;
        }

        private async Task<List<SnLeave>> GetFyListAsync(int pageIndex, int pageSize, bool isDesc)
        {
            if (isDesc)
            {
                return await _service.SnLeave.Where(s => true).OrderByDescending(c => c.Id).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            }
            else
            {
                return await _service.SnLeave.Where(s => true).OrderBy(c => c.Id).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            }
        }

        public async Task<bool> UpdateAsync(SnLeave entity)
        {
            _service.SnLeave.Update(entity);
            return await _service.SaveChangesAsync() > 0;
        }
    }
}
