using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Snblog.Cache.CacheUtil;
using Snblog.Enties.Models;
using Snblog.IRepository;
using Snblog.IService.IService;
using Snblog.Models;
using Snblog.Repository.Repository;

namespace Snblog.Service.Service
{
    public class SnNavigationService : ISnNavigationService
    {
        private readonly snblogContext _service;//DB
        private readonly CacheUtil _cacheutil;
        private int result_Int;
        private List<SnNavigation> result_List = null;
        private SnNavigation result_Model = null;
        public SnNavigationService( snblogContext service, ICacheUtil cacheutil) 
        {
            _service = service;
            _cacheutil = (CacheUtil)cacheutil;
        }

        /// <summary>
        /// 条件分页查询 - 支持排序
        /// </summary>
        /// <param name="type"></param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页记录条数</param>
        /// <param name="isDesc">是否倒序</param>
        public async Task<List<SnNavigation>> GetFyAllAsync(string type, int pageIndex, int pageSize, bool isDesc)
        {
            result_List = _cacheutil.CacheString1("GetFyAllAsync" + type + pageIndex + pageSize + isDesc, result_List);
            if (result_List == null)
            {
                await GetFyAll(type, pageIndex, pageSize, isDesc);
                _cacheutil.CacheString1("GetFyAllAsync" + type + pageIndex + pageSize + isDesc, result_List);
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
            var todoItem = await _service.SnNavigation.FindAsync(id);
            if (todoItem == null) return false;
            _service.SnNavigation.Remove(todoItem);
            return await _service.SaveChangesAsync() > 0;
         
        }




        public async Task<List<SnNavigation>> GetTypeOrderAsync(string type, bool order)
        {
            result_List = _cacheutil.CacheString1("GetTypeOrderAsync" + type + order, result_List);
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
                _cacheutil.CacheString1("GetTypeOrderAsync" + type + order, result_List);
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
            await _service.SnNavigation.AddAsync(entity);
            return await _service.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(SnNavigation entity)
        {
            _service.SnNavigation.Update(entity);
            return await _service.SaveChangesAsync() > 0;

        }

        public async Task<int> GetCountAsync()
        {
            result_Int = _cacheutil.CacheNumber1("GetCountAsync", result_Int);
            if (result_Int == 0)
            {
                result_Int = await _service.SnNavigation.CountAsync();
                _cacheutil.CacheNumber1("GetCountAsync", result_Int);
            }
            return result_Int;
        }
        /// <summary>
        /// 查询总数
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public async Task<int> CountTypeAsync(string type)
        {
            result_Int = _cacheutil.CacheNumber1("CountTypeAsync", result_Int);
            if (result_Int == 0)
            {
                result_Int = await _service.SnNavigation.CountAsync(c => c.NavType == type);
                _cacheutil.CacheNumber1("CountTypeAsync", result_Int);
            }
            return result_Int;
        }


        public async Task<List<SnNavigation>> GetAllAsync()
        {

            result_List = _cacheutil.CacheString1("GetAllAsync", result_List);
            if (result_List == null)
            {
                result_List = await _service.SnNavigation.ToListAsync();
                _cacheutil.CacheString1("GetAllAsync", result_List);
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

        public async Task<SnNavigation> GetByIdAsync(int id)
        {
            result_Model = _cacheutil.CacheString1("GetByIdAsync" + id, result_Model);
            if (result_Model == null)
            {
                result_Model = await _service.SnNavigation.FindAsync(id);
                _cacheutil.CacheString1("GetByIdAsync" + id, result_Model);
            }

            return result_Model;
        }
    }
}
