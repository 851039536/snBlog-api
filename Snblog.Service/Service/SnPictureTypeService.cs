using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Snblog.Cache.CacheUtil;
using Snblog.IService.IService;
using Snblog.Models;
using Snblog.Repository.Repository;

namespace Snblog.Service.Service
{
    public class SnPictureTypeService : ISnPictureTypeService
    {
        private readonly snblogContext _service;//DB
        private readonly CacheUtil _cacheutil;
        private int result_Int = default;
        private List<SnPictureType> result_List = default;
        public SnPictureTypeService(snblogContext service, ICacheUtil cacheutil)
        {
            _service = service;
            _cacheutil = (CacheUtil)cacheutil;
        }


        public async Task<bool> AddAsync(SnPictureType entity)
        {
            await _service.SnPictureType.AddAsync(entity);
            return await _service.SaveChangesAsync() > 0;
        }

        public async Task<List<SnPictureType>> GetAllAsync()
        {
            result_List = _cacheutil.CacheString("SnPictureType_GetAllAsync", result_List);
            if (result_List != null)
            {
                return result_List;
            }
            result_List = await _service.SnPictureType.ToListAsync();
            _cacheutil.CacheString("SnPictureType_GetAllAsync", result_List);
            return result_List;
        }

        public async Task<int> CountAsync()
        {
            result_Int = _cacheutil.CacheNumber("SnPictureType_CountAsync", result_Int);
            if (result_Int != 0)
            {
                return result_Int;
            }
            result_Int = await _service.SnPictureType.CountAsync();
            _cacheutil.CacheNumber("SnPictureType_CountAsync", result_Int);
            return result_Int;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            // _service.SnPictureType.Remove(Entity);
            //return await _service.SaveChangesAsync()>0;
            var todoItem = await _service.SnPictureType.FindAsync(id);
            _service.SnPictureType.Remove(todoItem);
            return await _service.SaveChangesAsync() > 0;
        }

        public async Task<SnPictureType> GetByIdAsync(int id)
        {
            SnPictureType result = default;
            result = _cacheutil.CacheString("SnPictureType_GetByIdAsync" + id, result);
            if (result != null)
            {
                return result;
            }
            result = await _service.SnPictureType.FindAsync(id);
            _cacheutil.CacheString("SnPictureType_GetByIdAsync" + id, result);
            return result;
        }

        public async Task<bool> UpdateAsync(SnPictureType entity)
        {
            _service.SnPictureType.Update(entity);
            return await _service.SaveChangesAsync() > 0;
        }

        public async Task<List<SnPictureType>> GetFyAllAsync(int pageIndex, int pageSize, bool isDesc)
        {
            result_List = _cacheutil.CacheString("SnPictureType_GetFyAllAsync" + pageIndex + pageSize + isDesc, result_List);
            if (result_List != null)
            {
                return result_List;
            }
            result_List = await GetFyAll(pageIndex, pageSize, isDesc);
            _cacheutil.CacheString("SnPictureType_GetFyAllAsync" + pageIndex + pageSize + isDesc, result_List);
            return result_List;
        }

        private async Task<List<SnPictureType>> GetFyAll(int pageIndex, int pageSize, bool isDesc)
        {
            if (isDesc)
            {
                return await _service.SnPictureType.OrderByDescending(c => c.Id).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            }
            else
            {
                return await _service.SnPictureType.OrderBy(c => c.Id).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            }
        }

        public Task<List<SnPictureType>> GetFyTypeAllAsync(int type, int pageIndex, int pageSize, bool isDesc)
        {
            throw new NotImplementedException();
        }
    }
}
