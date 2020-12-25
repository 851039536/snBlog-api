using Snblog.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Snblog.IService.IService
{
    public interface ISnleaveService
    {
        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        Task<List<SnLeave>> GetAllAsync();
        /// <summary>
        /// 主键查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<List<SnLeave>> GetAllAsync(int id);
        Task<List<SnLeave>> GetFyAllAsync(int pageIndex, int pageSize, bool isDesc);
        /// <summary>
        /// 查询总数
        /// </summary>
        /// <returns></returns>
        Task<int> CountAsync();
        /// 添加数据
        /// </summary>
        /// <param name="Entity"></param>
        /// <returns></returns>
        Task<bool> AddAsync(SnLeave Entity);
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
        Task<bool> UpdateAsync(SnLeave Entity);
    }
}
