using System.Collections.Generic;
using System.Threading.Tasks;
using Snblog.Models;

namespace Snblog.IService.IService
{
    public interface ISnLabelsService
    {
        /// <summary>
        /// 查询所有
        /// </summary>
        /// <param name="cache">是否开启缓存</param>
        /// <returns></returns>
        Task<List<SnLabels>> GetAllAsync(bool cache);

        /// <summary>
        /// 主键查询
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="cache">是否开启缓存</param>
        /// <returns></returns>
        Task<SnLabels> GetByIdAsync(int id,bool cache);

        /// <summary>
        /// 条件分页查询 - 支持排序
        /// </summary>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页记录条数</param>
        /// <param name="isDesc">是否倒序</param>
        ///   /// <param name="cache">是否开启缓存</param>
        Task<List<SnLabels>> GetfyAllAsync(int pageIndex, int pageSize, bool isDesc,bool cache);
        /// <summary>
        ///  查询总数
        /// </summary>
        /// <param name="cache">是否开启缓存</param>
        /// <returns></returns>
        Task<int> GetCountAsync(bool cache);

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <returns></returns>
        Task<bool> AddAsync(SnLabels entity);

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> UpdateAsync(SnLabels entity);

        /// <summary>
        /// 异步按id删除
        /// </summary>
        Task<bool> DeleteAsync(int id);
    }
}
