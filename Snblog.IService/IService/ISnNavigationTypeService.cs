using System.Collections.Generic;
using System.Threading.Tasks;
using Snblog.Models;

namespace Snblog.IService.IService
{
    public interface ISnNavigationTypeService
    {
         /// <summary>
         /// 查询所有
         /// </summary>
         /// <param name="cache">是否开启缓存</param>
         /// <returns></returns>
        Task<List<SnNavigationType>> GetAllAsync(bool cache);

        /// <summary>
        /// 主键查询
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="cache">是否开启缓存</param>
        /// <returns></returns>
        Task<SnNavigationType>GetByIdAsync(int id,bool cache);

        /// <summary>
        /// 条件分页查询
        /// </summary>
        /// <param name="type"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="isDesc"></param>
        /// <param name="cache"></param>
        /// <returns></returns>
        Task<List<SnNavigationType>> GetFyTypeAllAsync(string type, int pageIndex, int pageSize, bool isDesc,bool cache);

        /// <summary>
        ///  查询总数
        /// </summary>
        /// <param name="cache">是否开启缓存</param>
        /// <returns></returns>
        Task<int> CountAsync(bool cache);

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> AddAsync(SnNavigationType entity);

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <returns></returns>
        Task<bool> DeleteAsync(int id);

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> UpdateAsync(SnNavigationType entity);
    }
}
