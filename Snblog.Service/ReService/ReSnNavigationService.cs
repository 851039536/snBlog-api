using Snblog.Cache.CacheUtil;
using Snblog.IRepository;
using Snblog.IService.IReService;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Snblog.Models;

namespace Snblog.Service.ReService
{
    public class ReSnNavigationService : BaseService, IReSnNavigationService
    {
        private readonly CacheUtil _cacheutil;
        private int result_Int;
        private List<SnNavigation> result_List = null;
        private SnNavigation result_Model = null;
        public ReSnNavigationService(IRepositoryFactory repositoryFactory, IConcardContext mydbcontext, ICacheUtil cacheutil) : base(repositoryFactory, mydbcontext)
        {
            _cacheutil = (CacheUtil)cacheutil;
        }

        public async Task<List<SnNavigation>> GetAllAsync()
        {
            result_List = _cacheutil.CacheString1("ReGetAllAsync", result_List);
            if (result_List == null)
            {
                result_List = await CreateService<SnNavigation>().GetAllAsync();
                _cacheutil.CacheString1("ReGetAllAsync", result_List);
            }
            return result_List;
        }

        /// <summary>
        /// 主键查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<SnNavigation> GetByIdAsync(int id)
        {
            result_Model = _cacheutil.CacheString1("ReGetByIdAsync" + id, result_Model);
            if (result_Model == null)
            {
                result_Model = await CreateService<SnNavigation>().GetByIdAsync(id);
                _cacheutil.CacheString1("ReGetByIdAsync" + id, result_Model);
            }
            return result_Model;
        }

        /// <summary>
        /// 查询总数
        /// </summary>
        /// <returns></returns>
        public async Task<int> GetCountAsync()
        {
            result_Int = _cacheutil.CacheNumber1("ReGetCountAsync", result_Int);
            if (result_Int == 0)
            {
                result_Int = await CreateService<SnNavigation>().CountAsync();
                _cacheutil.CacheNumber1("ReGetCountAsync", result_Int);
            }
            return result_Int;
        }

        /// <summary>
        /// 查询分类总数
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public async Task<int> CountTypeAsync(string type)
        {
            result_Int = _cacheutil.CacheNumber1("ReCountTypeAsync", result_Int);
            if (result_Int == 0)
            {
                result_Int = await CreateService<SnNavigation>().CountAsync(c => c.NavType == type);
                _cacheutil.CacheNumber1("ReCountTypeAsync", result_Int);
            }
            return result_Int;
        }

        /// <summary>
        /// 去重查询
        /// </summary>
        /// <param name="type">查询条件</param>
        /// <returns></returns>
        public async Task<List<SnNavigation>> GetDistinct(string type)
        {
            result_List = _cacheutil.CacheString1("ReGetDistinct" + type, result_List);
            if (result_List == null)
            {
                result_List = await CreateService<SnNavigation>().Distinct(s => s.NavType == type).ToListAsync();
                _cacheutil.CacheString1("ReGetDistinct" + type, result_List);
            }
            return result_List;

        }

        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="type">条件</param>
        /// <param name="order">排序</param>
        /// <returns>List</returns>
        public async Task<List<SnNavigation>> GetTypeOrderAsync(string type, bool order)
        {
             result_List = _cacheutil.CacheString1("ReGetTypeOrderAsync" + type + order, result_List);
            if (result_List == null)
            {
                result_List = await CreateService<SnNavigation>().Where(c => c.NavType == type, s => s.NavId, order).ToListAsync();
                _cacheutil.CacheString1("ReGetTypeOrderAsync" + type + order, result_List);
            }
            return result_List;
           
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
           result_List = _cacheutil.CacheString1("ReGetFyAllAsync" + type + pageIndex + pageSize + isDesc, result_List);
            if (result_List == null)
            {
                 result_List= await FyAll(type, pageIndex, pageSize, isDesc);
                _cacheutil.CacheString1("ReGetFyAllAsync" + type + pageIndex + pageSize + isDesc ,result_List);
            }
            return result_List;
        }

        /// <summary>
        /// 异步添加数据
        /// </summary>
        /// <returns></returns>
        public async Task<SnNavigation> AddAsync(SnNavigation entity)
        {
            return await CreateService<SnNavigation>().AddAsync(entity) ;
        }

        /// <summary>
        /// 更新操作
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(SnNavigation entity)
        {
            return  await CreateService<SnNavigation>().UpdateAsync(entity)>0;
        }

        /// <summary>
        /// 按id删除
        /// </summary>
        public async Task<bool> DeleteAsync(int id)
        {
            return  await CreateService<SnNavigation>().DeleteAsync(id)>0;
        }

        private async Task<List<SnNavigation>> FyAll(string type, int pageIndex, int pageSize, bool isDesc)
        {
            if (type == "all")
            {
                var data = await CreateService<SnNavigation>().WherepageAsync(s => s.NavType != null, c => c.NavId, pageIndex, pageSize, isDesc);
                return data.ToList();
            }
            else
            {
                var data = await CreateService<SnNavigation>().WherepageAsync(s => s.NavType == type, c => c.NavId, pageIndex, pageSize, isDesc);
                return data.ToList();
            }
        }
    }
}
