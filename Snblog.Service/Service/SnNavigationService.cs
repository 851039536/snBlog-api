using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Snblog.Cache.CacheUtil;
using Snblog.IRepository;
using Snblog.IService.IService;
using Snblog.Models;

namespace Snblog.Service.Service
{
    public class SnNavigationService : BaseService, ISnNavigationService
    {
        private readonly snblogContext _service;//DB
        private readonly CacheUtil _cacheutil;
        private int result_Int;
        private List<SnNavigation> result_List = null;
        private SnNavigation result_Model = null;
        public SnNavigationService(IRepositoryFactory repositoryFactory, IconcardContext mydbcontext, snblogContext service, ICacheUtil cacheutil) : base(repositoryFactory, mydbcontext)
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
            result_List = _cacheutil.CacheString("GetFyAllAsync" + type + pageIndex + pageSize + isDesc, result_List);
            if (result_List == null)
            {
                await GetFyAll(type, pageIndex, pageSize, isDesc);
                _cacheutil.CacheString("GetFyAllAsync" + type + pageIndex + pageSize + isDesc, result_List);
            }
            return result_List;

            // var data = type == "all" ? CreateService<SnNavigation>().Wherepage(s => s.NavType != null, c => c.NavId, pageIndex, pageSize, out count, isDesc) : CreateService<SnNavigation>().Wherepage(s => s.NavType == type, c => c.NavId, pageIndex, pageSize, out count, isDesc);
            //
            // return data.ToList();
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
            result_List = _cacheutil.CacheString("GetTypeOrderAsync" + type + order, result_List);
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
                _cacheutil.CacheString("GetTypeOrderAsync" + type + order, result_List);
            }
            return result_List;

            //var data = CreateService<SnNavigation>().Where(c => c.NavType == type, s => s.NavId, order);
            //return await data.ToListAsync();
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
            //return await CreateService<SnNavigation>().AddAsync(entity);
        }

        public async Task<bool> UpdateAsync(SnNavigation entity)
        {
            _service.SnNavigation.Update(entity);
            return await _service.SaveChangesAsync() > 0;

        }

        public async Task<int> GetCountAsync()
        {
            result_Int = _cacheutil.CacheNumber("GetCountAsync", result_Int);
            if (result_Int == 0)
            {
                result_Int = await _service.SnNavigation.CountAsync();
                _cacheutil.CacheNumber("GetCountAsync", result_Int);
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
            result_Int = _cacheutil.CacheNumber("CountTypeAsync", result_Int);
            if (result_Int == 0)
            {
                result_Int = await _service.SnNavigation.CountAsync(c => c.NavType == type);
                _cacheutil.CacheNumber("CountTypeAsync", result_Int);
            }
            return result_Int;
            // return CreateService<SnNavigation>().Count(c => c.NavType == type);
        }


        public async Task<List<SnNavigation>> GetAllAsync()
        {

            result_List = _cacheutil.CacheString("GetAllAsync", result_List);
            if (result_List == null)
            {
                result_List = await _service.SnNavigation.ToListAsync();
                _cacheutil.CacheString("GetAllAsync", result_List);
            }

            return result_List;
        }

        public async Task<List<SnNavigation>> GetDistinct(string type)
        {
            result_List = _cacheutil.CacheString("GetDistinct" + type, result_List);
            if (result_List == null)
            {
                result_List = await _service.SnNavigation.Distinct().Where(c => c.NavType == type).ToListAsync();
                _cacheutil.CacheString("GetDistinct" + type, result_List);
            }
            return result_List;
            // var data = CreateService<SnNavigation>().Distinct(s => s.NavType == type);
            //
            // return data.ToList();
        }

        public async Task<SnNavigation> GetByIdAsync(int id)
        {
            result_Model = _cacheutil.CacheString("GetByIdAsync" + id, result_Model);
            if (result_Model == null)
            {
                result_Model = await _service.SnNavigation.FindAsync(id);
                _cacheutil.CacheString("GetByIdAsync" + id, result_Model);
            }

            return result_Model;
        }
    }
}
