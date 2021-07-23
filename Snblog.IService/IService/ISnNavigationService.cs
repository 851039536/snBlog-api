using System.Collections.Generic;
using System.Threading.Tasks;
using Snblog.Enties.Models;

namespace Snblog.IService.IService
{
    /// <summary>
    /// 业务类接口
    /// </summary>
    public interface ISnNavigationService
    {

        /// <summary>
        /// 查询总数
        /// </summary>
        /// <param name="cache">是否开启缓存</param>
        /// <returns></returns>
        Task<int> GetCountAsync(bool cache);

        /// <summary>
        /// 查询分类总数
        /// </summary>
        /// <param name="type"></param>
        /// <param name="cache">是否开启缓存</param>
        /// <returns></returns>
        Task<int> CountTypeAsync(string type, bool cache);
        /// <summary>
        /// 查询所有
        /// </summary>
        /// <param name="cache">是否开启缓存</param>
        /// <returns></returns>
        Task<List<SnNavigation>> GetAllAsync(bool cache);
        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="type">条件</param>
        /// <param name="order">排序</param>
        /// <param name="cache">是否开启缓存</param>
        /// <returns>List</returns>
        Task<List<SnNavigation>> GetTypeOrderAsync(string type, bool order, bool cache);

        /// <summary>
        /// 主键查询
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="cache">是否开启缓存</param>
        /// <returns></returns>
        Task<SnNavigation> GetByIdAsync(int id, bool cache);

        /// <summary>
        /// 条件分页查询 - 支持排序
        /// </summary>
        /// <param name="type"></param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页记录条数</param>
        /// <param name="isDesc">是否倒序</param>
        ///  <param name="cache">是否开启缓存</param>
        Task<List<SnNavigation>> GetFyAllAsync(string type, int pageIndex, int pageSize, bool isDesc,bool cache);

        /// <summary>
        /// 去重查询
        /// </summary>
        /// <param name="type">查询条件</param>
        /// <returns></returns>
        Task<List<SnNavigation>> GetDistinct(string type);




        /// <summary>
        /// 按id删除
        /// </summary>
        Task<bool> DeleteAsync(int id);



        /// <summary>
        /// 异步添加数据
        /// </summary>
        /// <returns></returns>
        Task<bool> AddAsync(SnNavigation entity);


        /// <summary>
        /// 更新操作
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> UpdateAsync(SnNavigation entity);


    }
}
