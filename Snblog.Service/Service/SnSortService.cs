using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Snblog.Cache.CacheUtil;
using Snblog.IRepository;
using Snblog.IService;
using Snblog.Models;
using Snblog.Repository.Repository;

namespace Snblog.Service.Service
{
    public class SnSortService : BaseService, ISnSortService
    {

        private readonly snblogContext _service;
        private readonly CacheUtil _cacheutil;
        private int result_Int;
        private List<SnSort> result_List = default;
        public SnSortService(IRepositoryFactory repositoryFactory, IconcardContext mydbcontext, snblogContext service, ICacheUtil cacheutil) : base(repositoryFactory, mydbcontext)
        {
            _service = service;
            _cacheutil = (CacheUtil)cacheutil;
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(int id)
        {
            var result=  await  _service.SnSort.FindAsync(id);
              if (result == null) return false;
            _service.SnSort.Remove(result);
             return await _service.SaveChangesAsync() > 0;
        }

        public async Task<List<SnSort>> AsyGetSort()
        {
            var data = CreateService<SnSort>();
            return await data.GetAll().ToListAsync();
        }

        public async Task<SnSort> GetByIdAsync(int id)
        {
            SnSort result = default;
            result = _cacheutil.CacheString("SnSort_GetByIdAsync" + id, result);
            if (result != null)
            {
                return result;
            }
            result = await _service.SnSort.FindAsync(id);
            _cacheutil.CacheString("SnSort_GetByIdAsync" + id, result);
            return result;
        }

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<bool> AddAsync(SnSort entity)
        {
            await  _service.SnSort.AddAsync(entity);
            return await _service.SaveChangesAsync()>0;
           
        }

        public async Task<bool> UpdateAsync(SnSort entity)
        {
            _service.SnSort.Update(entity);
            return await  _service.SaveChangesAsync()>0;
        }

        public async Task<List<SnSort>> GetFyAllAsync(int pageIndex, int pageSize,  bool isDesc)
        {
            result_List = _cacheutil.CacheString("SnSort_GetFyAllAsync"+pageIndex+pageSize+isDesc, result_List);
            if (result_List != null)
            {
                return result_List;
            }
            result_List = await GetFyAll(pageIndex, pageSize, isDesc);
           _cacheutil.CacheString("SnSort_GetFyAllAsync"+pageIndex+pageSize+isDesc, result_List);
            return result_List;
        }

        private async Task<List<SnSort>> GetFyAll(int pageIndex, int pageSize, bool isDesc)
        {
            if (isDesc)
            {
                result_List = await _service.SnSort.OrderByDescending(c => c.SortId).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            }
            else
            {
                result_List = await _service.SnSort.OrderBy(c => c.SortId).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            }

            return result_List;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public async Task<List<SnSort>> GetAllAsync()
        {
            result_List = _cacheutil.CacheString("SnSort_GetAllAsync", result_List);
            if (result_List != null)
            {
                return result_List;
            }
            result_List = await _service.SnSort.ToListAsync();
            _cacheutil.CacheString("SnSort_GetAllAsync", result_List);

            return result_List;
        }

        public async Task<int> GetCountAsync()
        {
            result_Int = _cacheutil.CacheNumber("SnSort_GetCountAsync", result_Int);
            if (result_Int != 0)
            {
                return result_Int;
            }
            result_Int = await _service.SnSort.CountAsync();
           _cacheutil.CacheNumber("SnSort_GetCountAsync", result_Int);

            return result_Int;
        }
    }
}
