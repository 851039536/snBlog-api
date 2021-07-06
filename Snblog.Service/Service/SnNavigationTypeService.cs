using Microsoft.EntityFrameworkCore;
using Snblog.IService.IService;
using Snblog.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Snblog.Cache.CacheUtil;
using Snblog.Enties.Models;
using Snblog.Repository.Repository;

namespace Snblog.Service.Service
{
    public class SnNavigationTypeService : ISnNavigationTypeService
    {
        private readonly snblogContext _service;//DB
        private readonly CacheUtil _cacheutil;
        private int result_Int;
        private List<SnNavigationType> result_List = null;
        public SnNavigationTypeService(snblogContext service, ICacheUtil cacheutil)
        {
            _service = service;
            _cacheutil = (CacheUtil)cacheutil;
        }

        public async Task<bool> AddAsync(SnNavigationType entity)
        {
            await _service.SnNavigationType.AddAsync(entity);
            return await _service.SaveChangesAsync() > 0;
        }

        public async Task<int> CountAsync()
        {
            result_Int = _cacheutil.CacheNumber1("SnNavigationType_CountAsync", result_Int);
            if (result_Int == 0)
            {
                result_Int = await _service.SnNavigationType.CountAsync();
                _cacheutil.CacheNumber1("SnNavigationType_CountAsync", result_Int);
            }
            return result_Int;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var todoItem = await _service.SnNavigationType.FindAsync(id);
            _service.SnNavigationType.Remove(todoItem);
            return await _service.SaveChangesAsync() > 0;
        }

        public async Task<List<SnNavigationType>> GetAllAsync()
        {
            result_List = _cacheutil.CacheString1("SnNavigationTypeGetAllAsync", result_List);
            if (result_List == null)
            {
                result_List = await _service.SnNavigationType.ToListAsync();
                _cacheutil.CacheString1("SnNavigationTypeGetAllAsync", result_List);
            }
            return result_List;
        }

        public async Task<SnNavigationType> GetByIdAsync(int id)
        {
            SnNavigationType result = default;
            result = _cacheutil.CacheString1("SnNavigationTypeGetByIdAsync"+id, result);
            if (result == null)
            {
                result = await _service.SnNavigationType.FindAsync(id);
                _cacheutil.CacheString1("SnNavigationTypeGetByIdAsync"+id, result);
            }
            return result;
        }

        public async Task<List<SnNavigationType>> GetFyTypeAllAsync(string type, int pageIndex, int pageSize, bool isDesc)
        {

            result_List = _cacheutil.CacheString1("SnNavigationTypeGetFyTypeAllAsync"+type+pageIndex+pageSize+isDesc, result_List);
            if (result_List == null)
            {
                result_List = await GetFyTypeAll(type, pageIndex, pageSize, isDesc);
               _cacheutil.CacheString1("SnNavigationTypeGetFyTypeAllAsync"+type+pageIndex+pageSize+isDesc, result_List);
            }
            return result_List;
        }

        private async Task<List<SnNavigationType>> GetFyTypeAll(string type, int pageIndex, int pageSize, bool isDesc)
        {
            if (isDesc)
            {
                if (type.Equals("all"))
                {
                    return await _service.SnNavigationType.Where(s => true).OrderByDescending(c => c.Id).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
                }
                else
                {
                    return await _service.SnNavigationType.Where(s => s.NavType == type).OrderByDescending(c => c.Id).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
                }
            }
            else
            {
                if (type.Equals("all"))
                {
                    return await _service.SnNavigationType.Where(s => s.NavType == type).OrderBy(c => c.Id).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
                }
                else
                {
                    return await _service.SnNavigationType.Where(s => true).OrderBy(c => c.Id).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
                }
            }
        }

        public async Task<bool> UpdateAsync(SnNavigationType entity)
        {
            _service.SnNavigationType.Update(entity);
            return await _service.SaveChangesAsync() > 0;
        }
    }
}
