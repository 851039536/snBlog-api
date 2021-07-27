using System.Collections.Generic;
using System.Threading.Tasks;
using Snblog.Models;

namespace Snblog.IService.IService
{
    public interface ISnOneTypeService
    {
        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        Task<List<SnOneType>> GetAllAsync();

        /// <summary>
        /// 主键查询
        /// </summary>
        /// <returns></returns>
        Task<SnOneType> GetByIdAsync(int id);

        /// <summary>
        /// 类别查询
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        Task<SnOneType> GetTypeAsync(int type);


        /// <summary>
        /// 查询总数
        /// </summary>
        /// <returns></returns>
        Task<int> CountAsync();

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> AddAsync(SnOneType entity);

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
        Task<bool>UpdateAsync(SnOneType entity);


    }
}
