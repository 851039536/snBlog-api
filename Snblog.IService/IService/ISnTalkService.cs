using System.Collections.Generic;
using System.Threading.Tasks;
using Snblog.Models;

namespace Snblog.IService.IService
{
    public interface ISnTalkService
    {
        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        Task<List<SnTalk>> GetAllAsync();

        /// <summary>
        /// 主键查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<List<SnTalk>> GetAllAsync(int id);

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="isDesc"></param>
        /// <returns></returns>
        Task<List<SnTalk>> GetFyAllAsync(int pageIndex, int pageSize, bool isDesc);
        /// <summary>
        /// 条件分页查询
        /// </summary>
        /// <param name="type"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="isDesc"></param>
        /// <returns></returns>
        Task<List<SnTalk>> GetFyTypeAllAsync(int type, int pageIndex, int pageSize, bool isDesc);

        /// <summary>
        /// 查询总数
        /// </summary>
        /// <returns></returns>
        Task<int> CountAsync();

        /// <summary>
        /// 条件查询总数
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        Task<int> CountAsync(int type);

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> AddAsync(SnTalk entity);

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteAsync(int id);

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> UpdateAsync(SnTalk entity);
    }
}
