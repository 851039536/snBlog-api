using Snblog.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Snblog.IService.IService
{
   public interface ISnNavigationTypeService
    {
         /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        Task<List<SnNavigationType>> GetAllAsync();

        /// <summary>
        /// 主键查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<List<SnNavigationType>> GetAllAsync(int id);

        /// <summary>
        /// 条件分页查询
        /// </summary>
        /// <param name="type"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="isDesc"></param>
        /// <returns></returns>
        Task<List<SnNavigationType>> GetFyTypeAllAsync(string type, int pageIndex, int pageSize, bool isDesc);

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
        Task<bool> AddAsync(SnNavigationType Entity);
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="Entity"></param>
        /// <returns></returns>
        Task<bool> DeleteAsync(int id);

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="Entity"></param>
        /// <returns></returns>
        Task<bool> UpdateAsync(SnNavigationType Entity);
    }
}
