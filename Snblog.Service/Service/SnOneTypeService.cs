using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Snblog.Cache.CacheUtil;
using Snblog.IService.IService;
using Snblog.Models;

namespace Snblog.Service.Service
{
    public class SnOneTypeService : ISnOneTypeService
    {
        private readonly snblogContext _service;
        private readonly CacheUtil _cacheutil;
        private int result_Int;
        private List<SnOneType> result_List = null;
        public SnOneTypeService(snblogContext service, ICacheUtil cacheutil)
        {
            _service = service;
            _cacheutil = (CacheUtil)cacheutil;
        }

        public async Task<bool> AddAsync(SnOneType entity)
        {
            await _service.SnOneType.AddAsync(entity);
            return await _service.SaveChangesAsync() > 0;
        }

        public async Task<int> CountAsync()
        {
            result_Int = _cacheutil.CacheNumber1("SnOneType_CountAsync", result_Int);
            if (result_Int != 0)
            {
                return result_Int;
            }
            result_Int = await _service.SnOneType.CountAsync();
            _cacheutil.CacheNumber1("SnOneType_CountAsync", result_Int);
            return result_Int;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            //_service.SnOneType.Remove(Entity);
            // return await _service.SaveChangesAsync()>0;
            var result = await _service.SnOneType.FindAsync(id);
            _service.SnOneType.Remove(result);
            return await _service.SaveChangesAsync() > 0;
        }

        public async Task<List<SnOneType>> GetAllAsync()
        {
            result_List = _cacheutil.CacheString1("SnOneType_GetAllAsync", result_List);
            if (result_List != null)
            {
                return result_List;
            }
            result_List = await _service.SnOneType.ToListAsync();
            _cacheutil.CacheString1("SnOneType_GetAllAsync", result_List);
            return result_List;
        }

        public async Task<SnOneType> GetByIdAsync(int id)
        {
            SnOneType result = default;
            result = _cacheutil.CacheString1("SnOneType_GetByIdAsync"+id, result);
            if (result != null)
            {
                return result;
            }
            result = await _service.SnOneType.FindAsync(id);
            _cacheutil.CacheString1("SnOneType_GetByIdAsync"+id, result);
            return result;
        }

        public async Task<SnOneType> GetTypeAsync(int type)
        {
            SnOneType result = default;
            result = _cacheutil.CacheString1("SnOneType_GetTypeAsync"+type, result);
            if (result != null)
            {
                return result;
            }
            result = await _service.SnOneType.FirstAsync(s => s.SoTypeId == type);
            _cacheutil.CacheString1("SnOneType_GetTypeAsync"+type, result);
            return result;
        }

        public async Task<bool> UpdateAsync(SnOneType entity)
        {
            _service.SnOneType.Update(entity);
            return await _service.SaveChangesAsync() > 0;
        }
    }
}
