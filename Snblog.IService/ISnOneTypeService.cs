using Snblog.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Snblog.IService
{
    public interface ISnOneTypeService
    {
        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        Task<List<SnOneType>> GetAll();

        /// <summary>
        /// 主键查询
        /// </summary>
        /// <returns></returns>
        Task<SnOneType> GetFirst(int id);

        /// <summary>
        /// 类别查询
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        Task<SnOneType> GetTypeFirst(int type);


        /// <summary>
        /// 查询总数
        /// </summary>
        /// <returns></returns>
        Task<int> CountAsync();

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="Entity"></param>
        /// <returns></returns>
        Task<bool> AddAsync(SnOneType Entity);

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="Entity"></param>
        /// <returns></returns>
        Task<bool> DeleteAsync(SnOneType Entity);

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="Entity"></param>
        /// <returns></returns>
        Task<bool>UpdateAsync(SnOneType Entity);

    }
}
