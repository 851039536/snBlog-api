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
    public class SnPictureService : ISnPictureService
    {
        private readonly snblogContext _service;//DB
        private readonly CacheUtil _cacheutil;
        private int result_Int;
        private List<SnPicture> result_List = default;
        public SnPictureService(snblogContext service, ICacheUtil cacheutil)
        {
            _service = service;
            _cacheutil = (CacheUtil)cacheutil;
        }

        public async Task<bool> AddAsync(SnPicture entity)
        {
            await _service.SnPicture.AddAsync(entity);
            return await _service.SaveChangesAsync() > 0;
        }

        public async Task<List<SnPicture>> GetAllAsync()
        {
            result_List = _cacheutil.CacheString1("SnPicture_GetAllAsync", result_List);
            if (result_List != null)
            {
                return result_List;
            }
            result_List = await _service.SnPicture.ToListAsync();
            _cacheutil.CacheString1("SnPicture_GetAllAsync", result_List);
            return result_List;
        }

        public async Task<int> CountAsync()
        {
             result_Int = _cacheutil.CacheNumber1("SnPicture_CountAsync", result_Int);
            if (result_Int != 0)
            {
                return result_Int;
            }
            result_Int = await _service.SnPicture.CountAsync();
            _cacheutil.CacheNumber1("SnPicture_CountAsync", result_Int);
            return result_Int;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            // _service.SnPicture.Remove(Entity);
            //return await _service.SaveChangesAsync()>0;
            //执行查询
            var todoItem = await _service.SnPicture.FindAsync(id);
            _service.SnPicture.Remove(todoItem);
            return await _service.SaveChangesAsync() > 0;

        }

        public async Task<SnPicture> GetByIdAsync(int id)
        {
            SnPicture result = default;
            result = _cacheutil.CacheString1("SnPicture_GetByIdAsync", result);
            if (result != null)
            {
                return result;
            }
            result = await _service.SnPicture.FindAsync(id);
            _cacheutil.CacheString1("SnPicture_GetByIdAsync", result);
            return result;
        }

        public async Task<bool> UpdateAsync(SnPicture entity)
        {
            _service.SnPicture.Update(entity);
            return await _service.SaveChangesAsync() > 0;
        }

        public async Task<List<SnPicture>> GetFyAllAsync(int pageIndex, int pageSize, bool isDesc)
        {
             result_List = _cacheutil.CacheString1("SnPicture_GetFyAllAsync"+pageIndex+pageSize+isDesc, result_List);
            if (result_List != null)
            {
                return result_List;
            }
            result_List =await GetFyAll(pageIndex, pageSize, isDesc);
             _cacheutil.CacheString1("SnPicture_GetFyAllAsync"+pageIndex+pageSize+isDesc, result_List);
            return result_List;
        }

        private async Task<List<SnPicture>> GetFyAll(int pageIndex, int pageSize, bool isDesc)
        {
            if (isDesc)
            {
                return await _service.SnPicture.Where(s => true).OrderByDescending(c => c.PictureId).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            }

            return await _service.SnPicture.Where(s => true).OrderBy(c => c.PictureId).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<List<SnPicture>> GetFyTypeAllAsync(int type, int pageIndex, int pageSize, bool isDesc)
        {
              result_List = _cacheutil.CacheString1("SnPicture_GetFyTypeAllAsync"+pageIndex+pageSize+isDesc+type, result_List);
            if (result_List != null)
            {
                return result_List;
            }
            result_List =await GetFyType(type, pageIndex, pageSize, isDesc);
            _cacheutil.CacheString1("SnPicture_GetFyTypeAllAsync"+pageIndex+pageSize+isDesc+type, result_List);
            return result_List;
        }

        private async Task<List<SnPicture>> GetFyType(int type, int pageIndex, int pageSize, bool isDesc)
        {
            if (isDesc)
            {
                return await _service.SnPicture.Where(s => s.PictureTypeId == type).OrderByDescending(c => c.PictureId).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            }
            else
            {
                return await _service.SnPicture.Where(s => s.PictureTypeId == type).OrderBy(c => c.PictureId).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            }
        }

        public async Task<int> CountAsync(int type)
        {
              result_Int = _cacheutil.CacheNumber1("SnPicture_CountAsync"+type, result_Int);
            if (result_Int != 0)
            {
                return result_Int;
            }
            result_Int =  await _service.SnPicture.CountAsync(s=>s.PictureTypeId==type);
        _cacheutil.CacheNumber1("SnPicture_CountAsync"+type, result_Int);
            return result_Int;
        }
    }
}
