using Snblog.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Snblog.IService
{
    public interface ISnVideoService
    {
        /// <summary>
        /// 查询所有
        /// </summary>
        /// <param name="cache">是否缓存</param>
        /// <returns></returns>
        Task<List<SnVideo>> GetAllAsync(bool cache);

        /// <summary>
        /// 查询总条数
        /// </summary>
        /// <param name="cache">是否开启缓存</param>
        /// <returns></returns>
        Task<int> GetCountAsync(bool cache);
        /// <summary>
        /// 按条件查询总数
        /// </summary>
        /// <param name="type">条件</param>
        /// <param name="cache">是否开启缓存</param>
        /// <returns></returns>
        Task<int> GetTypeCount(int type, bool cache);

        /// <summary>
        /// 主键查询
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="cache">是否开启缓存</param>
        /// <returns></returns>
        Task<SnVideo> GetByIdAsync(int id, bool cache);

        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="sortId">ID</param>
        /// <returns></returns>
        Task<List<SnVideo>> GetTypeAllAsync(int type, bool cache);

        /// <summary>
        /// 分页查询 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页记录条数</param>
        /// <param name="isDesc">是否倒序</param>
        ///  /// <param name="cache">是否开启缓存</param>
        Task<List<SnVideo>> GetFyAsync(int type, int pageIndex, int pageSize, bool isDesc, bool cache);



        /// <summary>
        /// 读取[字段/阅读/点赞]数量
        /// </summary>
        /// <returns></returns>
        Task<int> GetSumAsync(bool cache);
        /// <summary>
        /// 按id删除
        /// </summary>
        Task<bool> DeleteAsync(int id);

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="entity">对象</param>
        /// <returns></returns>
        Task<bool> AddAsync(SnVideo entity);

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="entity">对象</param>
        /// <returns></returns>
        Task<bool> UpdateAsync(SnVideo entity);

    }
}
